using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoryService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// Add default categories to a new user after registration.
        /// </summary>
        /// <param name="UserID">User ID (Guid as string)</param>
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
                    UserId = user.Id
                };

                user.UserCategories.Add(userCategory);
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Get ordered list of categories connected with user.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <param name="isSpending">Category spending type</param>
        /// <returns></returns>
        public List<CategoryModel> GetOrderedUserCategories(string userID, bool isSpending)
        {
            userID.CheckForNull(nameof(userID));
            
            int noCategoryID = isSpending ? Category.SpendingNoCategory : Category.IncomeNoCategory;

            var categories = _context.Categories
                .Where(c => c.IsSpendingCategory == isSpending
                        && c.UserCategories.Any(uc => uc.UserId == userID))
                .OrderBy(category => category.Name)
                .ToList();
            
            // "Без категории" should be first in a row.
            var defaultCategory = categories.First(c => c.ID == noCategoryID);
            categories.Remove(defaultCategory);
            categories.Insert(0, defaultCategory);

            return _mapper.Map<List<Category>, List<CategoryModel>>(categories);
        }

        /// <summary>
        /// Returns all users categories.
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns></returns>
        public List<CategoryModel> GetAllUserCategories(string userID)
        {
            userID.CheckForNull(nameof(userID));

            var categories = _context.Categories
                .Where(c => c.UserCategories
                    .Any(uc => uc.UserId == userID))
                .ToList();

            return _mapper.Map<List<Category>, List<CategoryModel>>(categories);
        }

        /// <summary>
        /// Get Category by ID.
        /// </summary>
        /// <param name="categoryID">Category ID</param>
        /// <returns></returns>
        public CategoryModel GetCategory(int categoryID)
        {
            categoryID.CheckMoreThenZero(nameof(categoryID));

            var category = _context.Categories.SingleOrDefault(c => c.ID == categoryID 
                    && !c.IsSystem
                    && c.CreatedByID != null
                    );
            
            return _mapper.Map<Category, CategoryModel>(category);
        }

        /// <summary>
        /// Add Category to users list.
        /// </summary>
        /// <param name="categoryModel"></param>
        public void AddNewCategory(CategoryModel categoryModel)
        {
            categoryModel.CheckForNull(nameof(categoryModel));

            var category = _mapper.Map<CategoryModel, Category>(categoryModel);
            _context.Categories.Add(category);
            category.UserCategories.Add(new UserCategory { CategoryID = category.ID, UserId = category.CreatedByID });
            
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete category from user list.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <param name="categoryID">Category ID</param>
        public void DeleteUserCategory(string userID, int categoryID)
        {
            userID.CheckForNull(nameof(userID));
            categoryID.CheckMoreThenZero(nameof(categoryID));

            var category = _context.Categories.SingleOrDefault(category => category.ID == categoryID);
            if (category == null)
            {
                throw new Exception("No Such Category.");
            }

            if (category.IsSystem)
            {
                throw new Exception("System Category can not be deleted.");
            }

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
                    var userCategory = _context.Users
                        .Include(u => u.UserCategories)
                        .SingleOrDefault(u => u.Id == userID)
                        .UserCategories
                        .SingleOrDefault(uc => uc.UserId == userID && uc.CategoryID == category.ID);

                    _context.Users
                        .Include(u => u.UserCategories)
                        .SingleOrDefault(u => u.Id == userID).UserCategories.Remove(userCategory);

                    // Delete from Categories if it's not common
                    if (!string.IsNullOrEmpty(category.CreatedByID))
                    {
                        _context.Categories.Remove(category);
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
    }
}
