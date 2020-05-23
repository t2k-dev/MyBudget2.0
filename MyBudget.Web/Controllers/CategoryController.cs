using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;
using MyBudget.Web.Models.Category;

namespace MyBudget.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        #region ctro & fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;

        public CategoryController(
            IHttpContextAccessor httpContextAccessor,
            ICategoryService categoryService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _categoryService = categoryService;
        }
        #endregion

        public IActionResult Index()
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var categories = _categoryService.GetAllUserCategories(userID);

            var viewmodel = new UserCategoriesViewModel
            {
                Categories = categories.Where(c => !c.IsSystem).ToList()
            };

            return View(viewmodel);
        }

        public IActionResult Add(bool isSpending)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var category = new CategoryModel()
            {
                IsSpendingCategory = isSpending,
                CreatedByID = userID
            };

            var viewmodel = new CategoryFormViewModel
            {
                Category = category
            };

            return View("CategoryForm", viewmodel);
        }

        [HttpPost]
        public IActionResult Save(CategoryModel category)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new CategoryFormViewModel
                {
                    Category = category
                };

                return View("CategoryForm", viewmodel);
            }

            if (category.ID == 0)
            {
                _categoryService.AddNewCategory(category);
            }
            else
            {
                _categoryService.UpdateCategory(category);
            }
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int id)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var category = _categoryService.GetCategory(id);

            if (category.CreatedByID != userID)
            {
                throw new Exception("Owners only can edit categories.");
            }

            var viewmodel = new CategoryFormViewModel
            {
                Category = category
            };

            return View("CategoryForm", viewmodel);
        }

        public IActionResult DeleteFromMyCategories(int id)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _categoryService.DeleteCategory(userID, id);

            return RedirectToAction("Index");
        }

    }
}