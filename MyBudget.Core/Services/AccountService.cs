﻿using MyBudget.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MyBudget.Core.Models;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Extensions;
using MyBudget.Domain;
using MyBudget.Core.Models.Account;

namespace MyBudget.Core.Services
{
    public class AccountService : IAccountService
    {
        #region ctors & fields

        private ApplicationContext _context;

        public AccountService(ApplicationContext context)
        {
            _context = context;
        }

        #endregion

        /// <summary>
        /// Save initial properties for the user.
        /// </summary>
        public void SaveInitialCustomization(InitialCustomizationModel model)
        {
            var currency = _context.Currencies.SingleOrDefault(currency => currency.Symbol == model.DefaultCurrency); // TODO: use ID on UI.

            var userInDb = _context.Users.Single(u => u.Id == model.UserID);
            userInDb.DefaultCurrency = currency;
            userInDb.CarryoverRests = model.CarryoverRests;
            
            _context.SaveChanges();
        }
    
        public string GetUserDefaultCurrencySymbol(string userID)
        {
            userID.CheckForNull(nameof(userID));
                
            var symbol = _context.Users
                .Where(user => user.Id == userID)
                .Select(u => u.DefaultCurrency.Symbol)
                .FirstOrDefault();

            return symbol;
        }

        public Currency GetUserDefaultCurrency(string userID)
        {
            userID.CheckForNull(nameof(userID));

            var currency = _context.Users
                .Where(user => user.Id == userID)
                .Select(u => u.DefaultCurrency)
                .FirstOrDefault();

            return currency;
        }

        public UserConfigs GetUserConfigs(string userID)
        {
            userID.CheckForNull(nameof(userID));

            var user = _context.Users.SingleOrDefault(u => u.Id == userID);
            
            var userConfigs = new UserConfigs
            {
                CarryoverRests = user.CarryoverRests,
                DefaultCurrencyID = user.DefaultCurrencyID
            };

            return userConfigs;
        }

        public void SaveConfig(UserConfigs userConfigs, string userID)
        {
            userConfigs.CheckForNull(nameof(userConfigs));

            var userInDbo = _context.Users.Single(u => u.Id == userID);
            userInDbo.DefaultCurrencyID = userConfigs.DefaultCurrencyID;
            userInDbo.CarryoverRests = userConfigs.CarryoverRests;
            
            _context.SaveChanges();
        }
    }
}