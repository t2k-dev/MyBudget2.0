using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;
using MyBudget.Web.Models.Graph;

namespace MyBudget.Web.Controllers
{
    public class GraphController : Controller
    {
        #region ctor & fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;

        public GraphController(IHttpContextAccessor httpContextAccessor, IAccountService  accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
        }
        #endregion

        public IActionResult Pie()
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new PieViewModel()
            {
                UserID = userID,
                DefaultCurrency = _accountService.GetUserDefaultCurrencySymbol(userID)
            };

            return View(viewModel);
        }
    }
}