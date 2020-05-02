using MyBudget.Core.Models;
using System.Collections.Generic;

namespace MyBudget.Core.Interfaces
{
    public interface ITransactionService
    {
        public TransactionModel GetTransaction(string transactionID);

        public List<TransactionModel> GetUserTransactions(string userID, int year, int month);

        public void AddTransaction(TransactionModel transaction);

        public void UpdateTransaction(TransactionModel transaction);

        public void DeleteTransaction(string transactionID);

        public void ChangePlannedStatus(string transactionID);

        public void AddRestTransaction(string userID);

        public void AddTemplateTransactions(string userID);
    }
}
