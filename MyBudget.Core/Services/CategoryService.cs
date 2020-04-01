using Microsoft.EntityFrameworkCore;
using MyBudget.Core.Extensions;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;
using MyBudget.Data;
using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBudget.Core.Services
{
    public class CategoryService : ICategoryService
    {
        #region ctors & fields

        private ApplicationContext _context;

        public CategoryService(ApplicationContext context)
        {
            _context = context;
        }

        #endregion

        /// <summary>
        /// Add default categories to a new user after registration.
        /// </summary>
        /// <param name="userID"></param>
        public void AddDefaultCategories(string userID)
        {
            userID.CheckForNull(nameof(userID));

            var user = _context.Users.Single(t => t.Id == userID);
            user.UserCategories = new List<UserCategory>();

            var startupCategories = _context.Categories
                .Where(category => (category.CreatedByID == null) || (category.IsSystem))
                .ToList();

            foreach (var category in startupCategories)
            {
                var userCategory = new UserCategory()
                {
                    CategoryID = category.ID,
                    UserID = Guid.Parse(user.Id) 
                };

                user.UserCategories.Add(userCategory);
            }
            
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        public void DeleteUserCategory(string userID, int categoryID)
        {
            if ((userID == null) || (categoryID < 1))
            {
                throw new Exception("Invalid arguments.");
            }

            var category = _context.Categories.SingleOrDefault(category => category.ID == categoryID);             
            if (category == null)
            {
                throw new Exception("No Such Category.");
            }
            if (category.IsSystem)
                throw new Exception("System Category can not be deleted.");

            var user = _context.Users.SingleOrDefault(user => user.Id == userID);
            if (user == null)
            {
                throw new Exception("No Such User.");
            }

            using (var SqlTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Update Transactions
                    int defaultCategoryID = category.IsSpendingCategory ? Category.SpendingNoCategory : Category.IncomeNoCategory;
                    var transactions = _context.Transactions.Where(transaction => transaction.CategoryID == category.ID).ToList();
                    
                    transactions.ForEach(t => t.CategoryID = defaultCategoryID);

                    // Delete from MyCategories
                    //user.Categories.Remove(category);

                    // Delete from Categories if it's not common
                    if (!string.IsNullOrEmpty(category.CreatedByID))
                    {
                      //  _context.Categories.Remove(_category);
                    }

                    _context.SaveChanges();
                    SqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    SqlTransaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get ordered list of categories connected with user.
        /// </summary>
        /// <param name="userID">User ID(Guid)</param>
        /// <param name="isSpending">Category spending type</param>
        /// <returns></returns>
        public List<Category> GetOrderedUserCategories(string userID, bool isSpending)
        {
            userID.CheckForNull(nameof(userID));
            
            int noCategoryID = isSpending ? Category.SpendingNoCategory : Category.IncomeNoCategory;

            var categories = _context.Categories
                .Where(c => c.IsSpendingCategory == isSpending
                        && c.UserCategories.Any(uc => uc.UserID == Guid.Parse(userID)))
                .OrderBy(category => category.Name)
                .ToList();
            
            // "Без категории" should be first in a row.
            var defaultCategory = categories.First(c => c.ID == noCategoryID);            
            categories.Remove(defaultCategory);            
            categories.Insert(0, defaultCategory);
            
            return categories;
        }

        public List<CategoryModel> GetAllUserCategories(string userID)
        {
            userID.CheckForNull(nameof(userID));

            var categories = _context.Categories
                .Where(c => c.UserCategories
                    .Any(uc => uc.UserID == Guid.Parse(userID)))
                .ToList();
            
            var categoryModelList = new List<CategoryModel>();
            foreach (var category in categories)
            {
                var categoryModel = new CategoryModel()
                {
                    ID =
                };
            }

            return categoryModelList;
        }
    }
}
