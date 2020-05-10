using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;
using MyBudget.Core.Models.Account;
using MyBudget.Core.Services;
using MyBudget.Domain;
using MyBudget.Web.Models.Account;


namespace MyBudget.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IHttpContextAccessor httpContextAccessor, 
            IAccountService accountService,
            ICategoryService categoryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _categoryService = categoryService;
        }

        #region Login

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // проверяем, принадлежит ли URL приложению
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("MainPage", "Transaction");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            
            return View(model);
        }

        #endregion

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Email = model.Email, DefaultCurrencyID = 1 };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _categoryService.AddDefaultCategories(user.Id);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Отправка сообщения электронной почты с этой ссылкой
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи", "Подтвердите вашу учетную запись, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");

                    return RedirectToAction("InitialCustomization", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        #endregion

        #region InitialCustomization

        public IActionResult InitialCustomization()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SaveInitialCustomization(InitialCustomizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var initialCustomizationModel = new InitialCustomizationModel()
            {
                CarryoverRests = model.CarryoverRests,
                DefaultCurrency = model.DefaultCurrency,
                UserID = userID
            };

            _accountService.SaveInitialCustomization(initialCustomizationModel);

            return RedirectToAction("MainPage", "Transaction");
        }

        #endregion

        #region LogOff
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Manage
        public IActionResult Manage()
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var userConfigs = _accountService.GetUserConfigs(userID);
            
            var viewModel = new MangeViewModel
            {
                DefaultCurrencyID = userConfigs.DefaultCurrencyID,
                CarryoverRests = userConfigs.CarryoverRests,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveConfig(MangeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            
            var userConfigs = new UserConfigs
            {
                CarryoverRests = model.CarryoverRests,
                DefaultCurrencyID = model.DefaultCurrencyID
            };
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _accountService.SaveConfig(userConfigs, userID);
         
            return RedirectToAction("MainPage", "Transaction");
        }
        #endregion
    }
}
