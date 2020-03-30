using MyBudget.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MyBudget.Core.Models;
using MyBudget.Core.Interfaces;

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
            var userInDb = _context.Users.Single(u => u.Id == model.UserID);
            userInDb.DefaultCurrency = model.DefaultCurrency;
            userInDb.CarryoverRests = model.CarryoverRests;
            
            _context.SaveChanges();
        }
    }
}
