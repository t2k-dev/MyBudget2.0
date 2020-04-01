using MyBudget.Core.Models;
using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface ITransactionService
    {
        public TransactionModel GetTransaction(string transactionID);

        public List<TransactionModel> GetUserTransactions(string userID, int year, int month);

        public void AddTransaction(TransactionModel transaction);

        public void UpdateTransaction(TransactionModel transaction);

        public void DeleteTransaction(string transactionID);
    }
}
