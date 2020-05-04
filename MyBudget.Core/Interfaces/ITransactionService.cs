using MyBudget.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudget.Core.Interfaces
{
    public interface ITransactionService
    {
        public TransactionModel GetTransaction(string transactionID);

        public List<TransactionModel> GetUserTransactions(string userID, int year, int month);

        public void AddTransaction(TransactionModel transaction);

        public Task<Guid> AddTransactionAsync(TransactionModel transactionModel);

        public void UpdateTransaction(TransactionModel transaction);

        public void DeleteTransaction(string transactionID);

        public void ChangePlannedStatus(string transactionID);

        public void AddRestTransaction(string userID);

        public void AddTemplateTransactions(string userID);
    }
}
