using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBudget.API.Filters;
using MyBudget.API.Models.Account;
using MyBudget.API.Models.Category;
using MyBudget.API.Models.Transaction;
using MyBudget.Core.Enums;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models.Account;
using MyBudget.Domain;

namespace MyBudget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region ctor & fields

        private readonly IAccountService _accountService;
        private readonly SignInManager<User> _signInManager;
        private readonly IAutoOperationsService _autoOperationsService;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;

        public AccountsController(
            IAccountService accountService,
            SignInManager<User> signInManager,
            IAutoOperationsService autoOperationsService,
            ICategoryService categoryService,
            ITransactionService transactionService
            )
        {
            _accountService = accountService;
            _signInManager = signInManager;
            _autoOperationsService = autoOperationsService;
            _categoryService = categoryService;
            _transactionService = transactionService;
        }

        #endregion

        [HttpPost]
        [CheckModelForNull]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO model)
        {
            try
            {
                var user = new UserModel {
                    UserName = model.Username,
                    Email = model.Email,
                    DefaultCurrencyID = (int)CommonCurrencies.Tenge
                };
                var id = await _accountService.RegisterAsync(user, model.Password);
                
                return Created("", id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [CheckModelForNull]
        [Route("changepassword")]
        public IActionResult ChangePassword([FromBody]ChangePasswordRequestDTO model)
        {
            try
            {
                _accountService.ChangePasswordAsync(model.UserId, model.OldPassword, model.NewPassword);

                return Ok("The password is changed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [CheckModelForNull]
        [Route("updateSettings/{userId}")]
        public IActionResult UpdateSettings(string userId, UserSettingsUpdateRequestDTO model)
        {
            try
            {
                var userConfig = _accountService.GetUserConfigs(userId);
                if (userConfig == null)
                {
                    return NotFound();
                }

                userConfig.DefaultCurrencyID= model.DefCurrency ?? userConfig.DefaultCurrencyID;
                userConfig.CarryoverRests = model.CarryoverRests ?? userConfig.CarryoverRests;

                _accountService.SaveConfig(userConfig, userId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [CheckModelForNull]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO model)
        {
            try
            {
                int statusCode = 200;

                var result = await _signInManager.PasswordSignInAsync(model.usr, model.pass, true, lockoutOnFailure: false);
                var userID = _accountService.GetUserIDByName(model.usr);

                if (result.Succeeded)
                {
                    _autoOperationsService.ExecuteMonthlyOperations(userID);
                    var userConfigs = _accountService.GetUserConfigs(userID);

                    var categories = _categoryService.GetAllUserCategories(userID);
                    var categoriesList = new List<CategoryDTO>();
                    foreach (var item in categories)
                    {
                        categoriesList.Add(new CategoryDTO
                        {
                            Id = item.ID,
                            Name = item.Name,
                            IsSpendingCategory = item.IsSpendingCategory,
                            IsSystem = item.IsSystem,
                            CreatedBy = item.CreatedByID, //TODO check
                            Icon = item.Icon
                        });
                    }

                    var transactions = _transactionService.GetUserTransactions(userID, DateTime.Now.Year, DateTime.Now.Month);
                    var transactionsList = new List<TransactionListItem>();
                    foreach (var transaction in transactions)
                    {
                        TransactionListItem listItem = new TransactionListItem()
                        {
                            Id = transaction.ID.ToString(),
                            Amount = transaction.Amount,
                            CategoryId = transaction.CategoryID,
                            IsPlaned = transaction.IsPlaned,
                            IsSpending = transaction.IsSpending,
                            Name = transaction.Name,
                            TransDate = transaction.TransactionDate,
                            UserId = transaction.UserID
                        };
                        transactionsList.Add(listItem);
                    }

                    LoginResponseDTO response = new LoginResponseDTO
                    {
                        Status = statusCode,
                        UserSettings = userConfigs,
                        Categories = categoriesList,
                        Transactions = transactionsList
                    };

                    return Ok(response);
                }
                else
                {
                    statusCode = 401;
                    LoginResponseDTO erroResult = new LoginResponseDTO
                    {
                        Status = 401
                    };
                    return Ok(erroResult);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [CheckModelForNull]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDTO model)
        {
            try
            {
                await _accountService.ForgotPassword(model.Username, model.Email);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("sendPasswordEmail")]
        public IActionResult sendPasswordEmail(string id)
        {

            var senderEmail = new MailAddress("MyBudgetTeam@yandex.kz", "MyBudgetTeam");
            var password = "nekruz";

            var receiverEmail = new MailAddress("t2k.ivan@gmail.com", "Receiver");

            var smtp = new SmtpClient
            {
                Host = "smtp.yandex.ru",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };

            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = "MyBudget восстановление пароля",
                Body = "Ваш пароль: "
            })
            {
                smtp.Send(mess);
            }

            return Ok();
        }
    }
}