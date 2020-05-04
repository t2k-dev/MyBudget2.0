using MyBudget.Data;
using MyBudget.Domain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyBudget.Core.Interfaces;
using System;
using MyBudget.Core.Models;
using AutoMapper;
using MyBudget.Core.Extensions;
using MyBudget.Core.Enums;
using System.Threading.Tasks;

namespace MyBudget.Core.Services
{
    public class TransactionService: ITransactionService
    {
        #region ctor & fields
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public TransactionService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion
        
        public TransactionModel GetTransaction(string transactionID)
        {
            var transaction = _context.Transactions
                .Include(t => t.Currency)
                .SingleOrDefault(t => t.ID == Guid.Parse(transactionID));

            return _mapper.Map<Transaction, TransactionModel>(transaction);
        }

        public List<TransactionModel> GetUserTransactions(string userID, int year, int month)
        {
            if (userID == null)
            {
                return null;
            }

            var transactions = _context.Transactions
                .Where(transaction => transaction.UserID == userID
                    && transaction.TransactionDate.Year == year
                    && transaction.TransactionDate.Month == month
                    )
                .OrderBy(transaction => transaction.TransactionDate)
                .Include(transaction => transaction.Category)
                .ToList();

            return _mapper.Map<List<Transaction>, List<TransactionModel>>(transactions);
        }

        public void AddTransaction(TransactionModel transactionModel)
        {
            transactionModel.CheckForNull(nameof(transactionModel));

            var transaction = _mapper.Map<TransactionModel, Transaction>(transactionModel);
            
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public async Task<Guid> AddTransactionAsync(TransactionModel transactionModel)
        {
            transactionModel.CheckForNull(nameof(transactionModel));

            var transaction = _mapper.Map<TransactionModel, Transaction>(transactionModel);

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            
            return transaction.ID;
        }

        public void UpdateTransaction(TransactionModel transactionModel)
        {
            transactionModel.CheckForNull(nameof(transactionModel));

            var transaction = _context.Transactions.SingleOrDefault(t => t.ID == transactionModel.ID);
            transaction.CheckForNull(nameof(transaction), transactionModel.ID.ToString());

            transaction.Name = transactionModel.Name;
            transaction.Amount = transactionModel.Amount;
            transaction.CategoryID = transactionModel.CategoryID;
            transaction.Description = transactionModel.Description;
            transaction.IsSpending = transactionModel.IsSpending;
            transaction.TransactionDate = transactionModel.TransactionDate;
            transaction.IsPlaned = transactionModel.IsPlaned;
            transaction.UserID = transactionModel.UserID;
            transaction.CurrencyID = transactionModel.CurrencyID;

            _context.SaveChanges();
        }

        public void DeleteTransaction(string id)
        {
            var transaction = _context.Transactions.SingleOrDefault(t => t.ID == Guid.Parse(id));
            transaction.CheckForNull(nameof(transaction), id);

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }

        public void ChangePlannedStatus(string transactionID)
        {
            var transaction = _context.Transactions.SingleOrDefault(t => t.ID == Guid.Parse(transactionID));
            transaction.CheckForNull(nameof(transaction), transactionID);
            transaction.IsPlaned = !transaction.IsPlaned;
            
            _context.SaveChanges();
        }

        public void AddRestTransaction(string userID)
        {
            var previousMonth = DateTime.Now.AddMonths(-1);
            var transactions = _context.Transactions
                    .Where(t => (t.UserID == userID) &&
                                (t.IsPlaned == false) &&
                                (t.TransactionDate.Month == previousMonth.Month) && (t.TransactionDate.Year == previousMonth.Year)
                          ).ToList();

            var sum = transactions.Where(x => x.IsSpending == false).Sum(x => x.Amount) - transactions.Where(x => x.IsSpending == true).Sum(x => x.Amount);

            if (sum <= 0)
            {
                return;
            }

            var transaction = new Transaction
            {
                Amount = sum,
                IsPlaned = false,
                IsSpending = false,
                Name = "Остаток за прошлый месяц",
                UserID = userID,
                TransactionDate = DateTime.Now,
                CategoryID = (int)SystemCategories.Rest
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void AddTemplateTransactions(string userID)
        {
            var templates = _context.Templates.Where(t => t.UserID == userID).ToList();

            if (templates.Count == 0)
            {
                return;
            }

            foreach (var template in templates)
            {
                var transDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, template.Day);
                var transaction = new Transaction
                {
                    Amount = template.Amount,
                    IsPlaned = true,
                    IsSpending = template.IsSpending,
                    Name = template.Name,
                    UserID = userID,
                    TransactionDate = transDate,
                    CategoryID = template.CategoryID,
                    CurrencyID = template.CurrencyID
                };

                _context.Transactions.Add(transaction);
            }
            _context.SaveChanges();
        }
    }
}
