using MyBudget.Core.Interfaces;
using MyBudget.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Services
{
    public class AutoOperationsService : IAutoOperationsService
    {
        #region ctor & fields
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public AutoOperationsService(
            IAccountService accountService,
            ITransactionService transactionService
            )
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }
        #endregion

        public void ExecuteMonthlyOperations(string userID)
        {
            var userConfigs = _accountService.GetUserConfigs(userID);

            if (!UpdateDateExpired(userConfigs.UpdateDate))
            {
                return;
            }

            if (userConfigs.CarryoverRests)
            {
                _transactionService.AddRestTransaction(userID);
            }
            _transactionService.AddTemplateTransactions(userID);
            _accountService.RefreshUpdateDate(userID);
        }

        #region Helpers
        private bool UpdateDateExpired(DateTime? updateDate)
        {
            if (updateDate == null)
            {
                return true;
            }

            if (updateDate.Value.Year < DateTime.Now.Year)
            {
                return true;
            }
            else if (updateDate.Value.Month < DateTime.Now.Month)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
