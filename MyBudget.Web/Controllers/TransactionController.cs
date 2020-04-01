using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;
using MyBudget.Domain;
using MyBudget.Web.Models.Transaction;

namespace MyBudget.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        #region ctor & fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IGoalService _goalService;

        public TransactionController(
            IHttpContextAccessor httpContextAccessor,
            ICategoryService categoryService,
            ITransactionService transactionService,
            IAccountService accountService,
            IGoalService goalService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _categoryService = categoryService;
            _transactionService = transactionService;
            _accountService = accountService;
            _goalService = goalService;
        }
        #endregion

        public IActionResult MainPage()
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var listDate = DateTime.Now;

            var viewModel = new MainPageViewModel()
            {
                DefaultCurrency = _accountService.GetUserDefaultCurrencySymbol(userID),
                ListDate = listDate.ToString("Y", new CultureInfo("ru-RU")),
                GoalsList = _goalService.GetUserGoals(userID)
            };

            return View(viewModel);
        }

        public IActionResult Add(bool isSpending)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var categories = _categoryService.GetOrderedUserCategories(userID, isSpending);
            var currency = _accountService.GetUserDefaultCurrency(userID);
            
            var transaction = new Transaction()
            {
                IsSpending = isSpending,
                TransactionDate = DateTime.Now,
                UserID = userID,
                Currency = currency, // TODO: refactor after adding multicurrency.
                CurrencyID = currency.ID
            };

            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = categories
            };

            return View("TransactionForm", viewModel);
        }

        public IActionResult Edit(string id)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var transaction = _transactionService.GetTransaction(id);
            
            /*if (transaction == null)
            {
                //return HttpNotFound();
            } */           

            var categories = _categoryService.GetOrderedUserCategories(userID, transaction.IsSpending);

            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = categories,
            };

            return View("TransactionForm", viewModel);
        }

        [HttpPost]
        public IActionResult Save(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var categories = _categoryService.GetOrderedUserCategories(userID, transaction.IsSpending);

                var viewModel = new TransactionFormViewModel()
                {
                    Transaction = transaction,
                    Categories = categories
                };

                return View("TransactionForm", viewModel);
            }

            if (transaction.ID == Guid.Empty)
            {
                _transactionService.AddTransaction(transaction);
            }
            else
            {
                _transactionService.UpdateTransaction(transaction);
            }

            return RedirectToAction("MainPage", "Transaction");
        }
    }
}