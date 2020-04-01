using MyBudget.Core.Models;
using MyBudget.Core.Models.Account;
using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Save initial properties for the user.
        /// </summary>
        public void SaveInitialCustomization(InitialCustomizationModel model);

        /// <summary>
        /// Gets the symbol of users default currency.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <returns>Symbol like '₸', '$', etc. </returns>
        public string GetUserDefaultCurrencySymbol(string userID);

        /// <summary>
        /// Gets the users default currency.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <returns>Currency</returns>
        public Currency GetUserDefaultCurrency(string userID);

        /// <summary>
        /// Get users configuration.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <returns>User Configs</returns>
        public UserConfigs GetUserConfigs(string userID);

        /// <summary>
        /// Save users configuration.
        /// </summary>
        /// <param name="userConfigs">Configs model</param>
        /// <param name="userID">User ID (Guid as string)</param>
        public void SaveConfig(UserConfigs userConfigs, string userID);
    }
}
