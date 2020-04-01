using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Enums;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;
using MyBudget.Domain;
using MyBudget.Web.Models;

namespace MyBudget.Web.Controllers
{
    public class GoalController : Controller
    {
        #region ctor & fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IGoalService _goalService;

        public GoalController(
            IHttpContextAccessor httpContextAccessor,
            ITransactionService transactionService,
            IAccountService accountService,
            IGoalService goalService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _transactionService = transactionService;
            _accountService = accountService;
            _goalService = goalService;
        }
        #endregion


        public IActionResult Add(byte type)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var defaultCurrency = _accountService.GetUserDefaultCurrency(userID);
            
            var goal = new GoalModel
            {
                Type = type,
                UserID = userID,
                IsActive = true,
                CurrencyID = defaultCurrency.ID,
                Currency = defaultCurrency
            };

            var viewModel = new GoalFormViewModel
            {
                Goal = goal
            };

            return View("GoalForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var goal = _goalService.GetGoal(id);
            
            var viewModel = new GoalFormViewModel
            {
                Goal = goal
            };

            return View("GoalForm", viewModel);
        }

        [HttpPost]
        public IActionResult Save(GoalModel goal)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!ModelState.IsValid)
            {
                var viewModel = new GoalFormViewModel
                {
                    Goal = goal,
                };

                return View("GoalForm", viewModel);
            }

            goal.IsActive = goal.CurrentAmount == goal.TotalAmount ? false : true;

            if (goal.ID == 0)
            {
                _goalService.AddGoal(goal);
            }
            else
            {
                _goalService.UpdateGoal(goal);
            }

            return RedirectToAction("MainPage", "Transaction");
        }

        [HttpPost]
        public IActionResult PutMoney(double Amount, int putOnId)
        {
            _goalService.PutMoney(Amount, putOnId);

            return RedirectToAction("MainPage", "Transaction");
        }

    }
}