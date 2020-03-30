using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Add default categories to a new user after registration.
        /// </summary>
        /// <param name="UserID"></param>
        public void AddDefaultCategories(string UserID);

        /// <summary>
        /// Get ordered list of categories connected with user.
        /// </summary>
        /// <param name="userID">User ID(Guid)</param>
        /// <param name="isSpending">Category spending type</param>
        /// <returns></returns>
        public List<Category> GetOrderedUserCategories(string userID, bool isSpending);

    }
}
