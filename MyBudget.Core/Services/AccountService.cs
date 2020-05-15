using MyBudget.Data;
using System.Linq;
using MyBudget.Core.Models;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Extensions;
using MyBudget.Domain;
using MyBudget.Core.Models.Account;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MyBudget.Core.Services
{
    public class AccountService : IAccountService
    {
        #region ctors & fields

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ICategoryService _categoryService;
        private readonly IEmailSenderService _emailSenderService; 

        public AccountService(
            ApplicationContext context,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ICategoryService categoryService,
            IEmailSenderService emailSenderService
            )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _categoryService = categoryService;
            _emailSenderService = emailSenderService;
        }

        #endregion

        /// <summary>
        /// Save initial properties for the user.
        /// </summary>
        public void SaveInitialCustomization(InitialCustomizationModel model)
        {
            var currency = _context.Currencies.SingleOrDefault(currency => currency.Symbol == model.DefaultCurrency); // TODO: use ID on UI.

            var userInDb = _context.Users.Single(u => u.Id == model.UserID);
            userInDb.DefaultCurrency = currency;
            userInDb.CarryoverRests = model.CarryoverRests;
            
            _context.SaveChanges();
        }
    
        public string GetUserDefaultCurrencySymbol(string userID)
        {
            userID.CheckForNull(nameof(userID));
                
            var symbol = _context.Users
                .Where(user => user.Id == userID)
                .Select(u => u.DefaultCurrency.Symbol)
                .FirstOrDefault();

            return symbol;
        }

        public string GetUserIDByName(string userName)
        {
            // TODO check
            return _context.Users
                .Where(u => u.UserName == userName)
                .Select(u => u.Id)
                .SingleOrDefault();
        }

        public CurrencyModel GetUserDefaultCurrency(string userID)
        {
            userID.CheckForNull(nameof(userID));

            var currency = _context.Users
                .Where(user => user.Id == userID)
                .Select(u => u.DefaultCurrency)
                .FirstOrDefault();

            return _mapper.Map<Currency, CurrencyModel>(currency);
        }

        public UserConfigs GetUserConfigs(string userID)
        {
            userID.CheckForNull(nameof(userID));

            var user = _context.Users.SingleOrDefault(u => u.Id == userID);
            
            var userConfigs = new UserConfigs
            {
                CarryoverRests = user.CarryoverRests,
                DefaultCurrencyID = user.DefaultCurrencyID,
                UpdateDate = user.UpdateDate
            };
            
            return userConfigs;
        }

        public void SaveConfig(UserConfigs userConfigs, string userID)
        {
            userConfigs.CheckForNull(nameof(userConfigs));

            var userInDbo = _context.Users.Single(u => u.Id == userID);
            userInDbo.DefaultCurrencyID = userConfigs.DefaultCurrencyID;
            userInDbo.CarryoverRests = userConfigs.CarryoverRests;
            
            _context.SaveChanges();
        }

        public void RefreshUpdateDate(string userID)
        {
            var userInDbo = _context.Users.Single(u => u.Id == userID);

            userInDbo.UpdateDate = DateTime.Now;

            _context.SaveChanges();
        }

        public async Task<string> RegisterAsync(UserModel userModel, string password)
        {
            var user = new User
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                DefaultCurrencyID = userModel.DefaultCurrencyID
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                _categoryService.AddDefaultCategories(user.Id);
                _emailSenderService.SendRegistrationEmail(user.Email);
            }
            else
            {
                string _errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    _errors = $"{_errors}{error};";
                }

                throw new Exception(_errors);
            }

            return user.Id;
        }

        public async void ChangePasswordAsync(string userID, string oldPassword, string newPassword)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userID);
            user.CheckForNull(nameof(user), userID);

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
    
        public void ForgotPassword(string userName, string email)
        {
            throw new Exception("Not implemented");

            var user = new User();
            if (!string.IsNullOrEmpty(email))
            {
                //user = _userManager.FindByEmailAsync(email);
            }
            else if (!string.IsNullOrEmpty(userName))
            {
                //user = _userManager.FindByNameAsync(userName);
            }

            if (user == null)
            {
                throw new Exception("User not found");
            }

            //string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            string newPassord = StringUtils.GeneratePassword();

            //var resetResul = await _userManager.ResetPasswordAsync(user, code, newPassord);

            //await AppUserManager.SendEmailAsync(user.Id, "Сброс пароля для MuBudget", $"Ваш временный пароль: {newPassord}");
        }
    }
}
