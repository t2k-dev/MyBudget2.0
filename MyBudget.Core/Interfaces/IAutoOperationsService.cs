using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface IAutoOperationsService
    {
        /// <summary>
        /// Run all automatic operations.
        /// </summary>
        /// <param name="userID"></param>
        public void ExecuteMonthlyOperations(string userID);
    }
}
