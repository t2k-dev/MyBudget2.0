using Microsoft.EntityFrameworkCore;
using MyBudget.Core.Enums;
using MyBudget.Core.Extensions;
using MyBudget.Core.Interfaces;
using MyBudget.Data;
using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBudget.Core.Services
{
    public class GoalService : IGoalService
    {
        #region ctor & fields
        private readonly ApplicationContext _context;
        private readonly IAccountService _accountService;

        public GoalService(
            ApplicationContext context,
            IAccountService accountService
            )
        {
            _context = context;
            _accountService = accountService;
        }
        #endregion

        public List<Goal> GetUserGoals(string userID)
        {
            if (userID == null)
            {
                return null;
            }

            var goals = _context.Goals.Where(g => g.UserID == userID)
                .OrderByDescending(g => g.IsActive)
                .ThenBy(g => g.GoalName)
                .ToList();

            return goals;
        }

        public Goal GetGoal(int goalID)
        {
            var goal = _context.Goals
                .Include(g => g.Currency)
                .SingleOrDefault(g => g.ID == goalID);

            return goal;
        }

        public void AddGoal(Goal goal)
        {
            goal.CheckForNull(nameof(goal));
            
            switch (goal.Type)
            {
                case (byte)GoalTypes.Debt:
                    {
                        CreateDebtTransaction(goal);
                        break;
                    }
                case (byte)GoalTypes.Loan:
                    {
                        CreateLoanTransaction(goal);
                        break;
                    }
            }

            _context.Goals.Add(goal);
            _context.SaveChanges();

        }

        public void UpdateGoal(Goal goal)
        {
            var goalInDb = _context.Goals.Single(g => g.ID == goal.ID);
            goalInDb.GoalName = goal.GoalName;
            goalInDb.TotalAmount = goal.TotalAmount;
            goalInDb.CurrentAmount = goal.CurrentAmount;
            goalInDb.CompleteDate = goal.CompleteDate;
            goalInDb.IsActive = goal.IsActive;

            _context.SaveChanges();
        }

        public void PutMoney(double amount, int goalID)
        {
            var goal = _context.Goals
                .SingleOrDefault(g => g.ID == goalID);

            goal.CurrentAmount += amount;

            if (goal.TotalAmount == goal.CurrentAmount)
            {
                goal.IsActive = false;
            }
            
            var isSpending = goal.Type == (byte)GoalTypes.Loan ? false : true;
            int categoryId = GetTransactionCategory(goal);

            Transaction transaction = new Transaction
            {
                Amount = amount,
                CategoryID = categoryId,
                IsSpending = isSpending,
                Name = $"Пополнение для \"{goal.GoalName}\"",
                UserID = goal.UserID,
                TransactionDate = DateTime.Now,
                IsPlaned = false,
                CurrencyID = goal.CurrencyID
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        #region Helpers
        private void CreateDebtTransaction(Goal goal)
        {
            goal.CheckForNull(nameof(goal));
            var currency = _accountService.GetUserDefaultCurrency(goal.UserID);

            Transaction transaction = new Transaction
            {
                Amount = goal.TotalAmount,
                CategoryID = Category.TakeDebt,
                IsSpending = false,
                Name = $"Взять в долг \"{goal.GoalName}\"",
                UserID = goal.UserID,
                TransactionDate = DateTime.Now,
                IsPlaned = false,
                CurrencyID = currency.ID
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        private void CreateLoanTransaction(Goal goal)
        {
            goal.CheckForNull(nameof(goal));
            var currency = _accountService.GetUserDefaultCurrency(goal.UserID);

            Transaction transaction = new Transaction
            {
                Amount = goal.TotalAmount,
                CategoryID = Category.GiveCredit,
                IsSpending = true,
                Name = $"Дать в долг \"{goal.GoalName}\"",
                UserID = goal.UserID,
                TransactionDate = DateTime.Now,
                IsPlaned = false,
                CurrencyID = currency.ID
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        private int GetTransactionCategory(Goal goal)
        {
            goal.CheckForNull(nameof(goal));

            int categoryId = 0;

            switch (goal.Type)
            {
                case (byte)GoalTypes.Debt:
                    {
                        categoryId = Category.PayCredit;
                        break;
                    }
                case (byte)GoalTypes.Loan:
                    {
                        categoryId = Category.RecieveDebt;
                        break;
                    }
                case (byte)GoalTypes.Goal:
                    {
                        categoryId =  Category.PayGoal;
                        break;
                    }
            }

            return categoryId;
        }
        #endregion
    }
}
