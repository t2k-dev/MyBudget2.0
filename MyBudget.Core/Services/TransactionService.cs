using MyBudget.Data;
using MyBudget.Domain;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyBudget.Core.Interfaces;

namespace MyBudget.Core.Services
{
    public class TransactionService: ITransactionService
    {
        #region ctor & fields
        private readonly ApplicationContext _context;

        public TransactionService(ApplicationContext context)
        {
            _context = context;
        }
        #endregion

        public List<Transaction> GetUserTransaction(string userID, int year, int month)
        {
            if (userID == null)
            {
                return null;
            }

            var transactions = _context.Transactions
                .Where(transaction => transaction.UserID == userID
                    && transaction.TransactionDate.Year == year
                    && transaction.TransactionDate.Month == month)
                .Include(transaction => transaction.Category)
                .ToList();

            return transactions;
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            var transactionInDb = _context.Transactions.Single(t => t.ID == transaction.ID);
            transactionInDb.Name = transaction.Name;
            transactionInDb.Amount = transaction.Amount;
            transactionInDb.CategoryID = transaction.CategoryID;
            transactionInDb.Description = transaction.Description;
            transactionInDb.IsSpending = transaction.IsSpending;
            transactionInDb.TransactionDate = transaction.TransactionDate;
            transactionInDb.IsPlaned = transaction.IsPlaned;
            transactionInDb.UserID = transaction.UserID;

            _context.SaveChanges();
        }

    }
}
