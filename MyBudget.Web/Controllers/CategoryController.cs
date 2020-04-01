using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;
using MyBudget.Web.Models.Category;

namespace MyBudget.Web.Controllers
{
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
    }
}