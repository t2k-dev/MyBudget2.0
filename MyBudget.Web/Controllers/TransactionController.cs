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
    public class TransactionController : Controller
    {
        #region ctor & fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;

        public TransactionController(
            IHttpContextAccessor httpContextAccessor,
            ICategoryService categoryService,
            ITransactionService transactionService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _categoryService = categoryService;
            _transactionService = transactionService;
        }
        #endregion

        [Authorize]
        public IActionResult MainPage()
        {
            var listDate = DateTime.Now;

            var viewModel = new MainPageViewModel()
            {
                DefaultCurrency = "₸",
                ListDate = listDate.ToString("Y", new CultureInfo("ru-RU")),
                MyGoals = new List<string>()
            };
            return View(viewModel);
        }

        public IActionResult Add(bool isSpending)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var categories = _categoryService.GetOrderedUserCategories(userID, isSpending);

            var transaction = new Transaction()
            {
                IsSpending = isSpending,
                TransactionDate = DateTime.Now,
                UserID = userID
            };

            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = categories
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