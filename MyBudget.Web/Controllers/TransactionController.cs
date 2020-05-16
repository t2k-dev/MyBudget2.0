using System;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Extensions;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;
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
        private readonly IAutoOperationsService _autoOperationsService;
        private readonly IExcelExportService _excelExportService;

        public TransactionController(
            IHttpContextAccessor httpContextAccessor,
            ICategoryService categoryService,
            ITransactionService transactionService,
            IAccountService accountService,
            IGoalService goalService,
            IAutoOperationsService autoOperationsService,
            IExcelExportService excelExportService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _categoryService = categoryService;
            _transactionService = transactionService;
            _accountService = accountService;
            _goalService = goalService;
            _autoOperationsService = autoOperationsService;
            _excelExportService = excelExportService;
        }
        #endregion

        public IActionResult MainPage(string id)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var listDate = string.IsNullOrWhiteSpace(id)
                ? DateTime.Now
                : DateTime.ParseExact(id, "MMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

            _autoOperationsService.ExecuteMonthlyOperations(userID);

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

            var transaction = new TransactionModel()
            {
                IsSpending = isSpending,
                TransactionDate = DateTime.Now,
                UserID = userID,
                CurrencyID = currency.ID
            };

            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = categories,
                DefaultCurrencySymbol = _accountService.GetUserDefaultCurrencySymbol(userID)
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
                DefaultCurrencySymbol = _accountService.GetUserDefaultCurrencySymbol(userID)
            };

            return View("TransactionForm", viewModel);
        }

        [HttpPost]
        public IActionResult Save(TransactionModel transaction)
        {
            if (!ModelState.IsValid)
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var categories = _categoryService.GetOrderedUserCategories(userID, transaction.IsSpending);

                var viewModel = new TransactionFormViewModel()
                {
                    Transaction = transaction,
                    Categories = categories,
                    DefaultCurrencySymbol = _accountService.GetUserDefaultCurrencySymbol(userID)
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

        [HttpPost]
        public IActionResult Delete(string id)
        {
            id.CheckForNull(nameof(id));

            _transactionService.DeleteTransaction(id);

            return RedirectToAction("MainPage");
        }

        public IActionResult ExportToExcel(DateTime? excelSince, DateTime? excelTill)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var stream = _excelExportService.GetTransactionsListFile(userID, excelSince, excelTill);

            if (stream == null)
            {
                return NotFound();
            }

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyBudget.xlsx");
        }
    }
}