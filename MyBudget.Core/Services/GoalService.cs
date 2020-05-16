using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBudget.Core.Enums;
using MyBudget.Core.Extensions;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;
using MyBudget.Data;
using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.Core.Services
{
    public class GoalService : IGoalService
    {
        #region ctor & fields
        private readonly ApplicationContext _context;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public GoalService(
            ApplicationContext context,
            IAccountService accountService,
            IMapper mapper
            )
        {
            _context = context;
            _accountService = accountService;
            _mapper = mapper;
        }
        #endregion

        public List<GoalModel> GetUserGoals(string userID)
        {
            if (userID == null)
            {
                return null;
            }

            var goals = _context.Goals.Where(g => g.UserID == userID)
                .OrderByDescending(g => g.IsActive)
                .ThenBy(g => g.GoalName)
                .ToList();

            return _mapper.Map<List<Goal>, List<GoalModel>>(goals);
        }

        public GoalModel GetGoal(int goalID)
        {
            var goal = _context.Goals
                .Include(g => g.Currency)
                .SingleOrDefault(g => g.ID == goalID);

            return _mapper.Map<Goal, GoalModel>(goal);
        }

        public void AddGoal(GoalModel goalModel)
        {
            goalModel.CheckForNull(nameof(goalModel));
            
            var goal = _mapper.Map<GoalModel, Goal>(goalModel);

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

        public async Task<int> AddGoalAsync(GoalModel goalModel)
        {
            goalModel.CheckForNull(nameof(goalModel));

            var goal = _mapper.Map<GoalModel, Goal>(goalModel);

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
            await _context.SaveChangesAsync();

            return goal.ID;
        }

        public void UpdateGoal(GoalModel goalModel)
        {
            goalModel.CheckForNull(nameof(goalModel));

            var goal = _mapper.Map<GoalModel, Goal>(goalModel);
            
            goal.IsActive = goal.CurrentAmount < goal.TotalAmount ? true : false;

            var goalInDb = _context.Goals.Single(g => g.ID == goal.ID);
            goalInDb.GoalName = goal.GoalName;
            goalInDb.TotalAmount = goal.TotalAmount;
            goalInDb.CurrentAmount = goal.CurrentAmount;
            goalInDb.CompleteDate = goal.CompleteDate;
            goalInDb.IsActive = goal.IsActive;

            _context.SaveChanges();
        }

        public void DeleteGoal(int id)
        {
            var goal = _context.Goals.SingleOrDefault(g => g.ID == id);
            goal.CheckForNull(nameof(goal), id.ToString());

            _context.Goals.Remove(goal);
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
