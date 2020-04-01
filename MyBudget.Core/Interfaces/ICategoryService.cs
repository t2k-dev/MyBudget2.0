using MyBudget.Core.Models;
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
        /// <param name="UserID">User ID (Guid as string)</param>
        public void AddDefaultCategories(string UserID);

        /// <summary>
        /// Get ordered list of categories connected with user.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <param name="isSpending">Category spending type</param>
        /// <returns></returns>
        public List<CategoryModel> GetOrderedUserCategories(string userID, bool isSpending);

        /// <summary>
        /// Get all user categories
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <returns></returns>
        public List<CategoryModel> GetAllUserCategories(string userID);

        /// <summary>
        /// Get Category by ID.
        /// </summary>
        /// <param name="categoryID">Category ID</param>
        /// <returns></returns>
        public CategoryModel GetCategory(int categoryID);

        /// <summary>
        /// Add Category to users list.
        /// </summary>
        /// <param name="categoryModel"></param>
        public void AddNewCategory(CategoryModel categoryModel);

        /// <summary>
        /// Delete category from user list.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <param name="categoryID">Category ID</param>
        public void DeleteUserCategory(string userID, int categoryID);
    }
}
