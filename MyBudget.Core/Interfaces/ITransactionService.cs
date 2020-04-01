﻿using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface ITransactionService
    {
        public Transaction GetTransaction(string transactionID);

        public List<Transaction> GetUserTransactions(string userID, int year, int month);

        public void AddTransaction(Transaction transaction);

        public void UpdateTransaction(Transaction transaction);
    }
}