using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;
using MyBudget.Core.Extensions;
using MyBudget.Web.Models.Template;

namespace MyBudget.Web.Controllers
{
    public class TemplateController : Controller
    {
        #region ctor & fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly ITemplateService _templateService;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IGoalService _goalService;

        public TemplateController(
            IHttpContextAccessor httpContextAccessor,
            ITemplateService templateService,
            ICategoryService categoryService,
            ITransactionService transactionService,
            IAccountService accountService,
            IGoalService goalService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _templateService = templateService;
            _categoryService = categoryService;
            _transactionService = transactionService;
            _accountService = accountService;
            _goalService = goalService;
        }
        #endregion

        public IActionResult TemplateList()
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = new TemplateListViewModel
            {
                Templates = _templateService.GetTemplates(userID),
            };

            return View(viewModel);
        }

        public IActionResult Add(bool isSpending)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currency = _accountService.GetUserDefaultCurrency(userID);
            var categories = _categoryService.GetOrderedUserCategories(userID, isSpending);
            
            List<int> days = new List<int>();
            for (int i = 1; i <= 28; i++)
            {
                days.Add(i);
            }

            var template = new TemplateModel()
            {
                IsSpending = isSpending,
                UserID = userID,
                Currency = currency, // TODO: refactor after adding multicurrency.
                CurrencyID = currency.ID
            };


            var viewModel = new TemplateFormViewModel
            {
                Template = template,
                Categories = categories,
                Days = new SelectList(days)
            };

            return View("TemplateForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var template = _templateService.GetTemplate(id);
            var categories = _categoryService.GetOrderedUserCategories(userID, template.IsSpending);

            // Список дней
            List<int> days = new List<int>();
            for (int i = 1; i <= 28; i++)
            {
                days.Add(i);
            }

            var viewModel = new TemplateFormViewModel
            {
                Template = template,
                Categories = categories,
                Days = new SelectList(days)
            };

            return View("TemplateForm", viewModel);
        }

        [HttpPost]
        public IActionResult Save(TemplateModel template)
        {
            if (!ModelState.IsValid)
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var categories = _categoryService.GetOrderedUserCategories(userID, template.IsSpending);

                var viewModel = new TemplateFormViewModel()
                {
                    Template = template,
                    Categories = categories
                };

                return View("TemplateForm", viewModel);
            }

            if (template.ID == 0)
            {
                _templateService.AddTemplate(template);
            }
            else
            {
                _templateService.UpdateTransaction(template);
            }

            return RedirectToAction("TemplateList", "Template");
        }

        public IActionResult Delete(int id)
        {
            id.CheckMoreThenZero(nameof(id));

            _templateService.DeleteTemplate(id);

            return RedirectToAction("TemplateList", "Template");

        }
    }
}