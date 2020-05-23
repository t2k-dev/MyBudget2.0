using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;
using MyBudget.Web.Models.Api;

namespace MyBudget.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        #region ctor & fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionService _transactionService;

        public TransactionController(
            IHttpContextAccessor httpContextAccessor,
            ITransactionService transactionService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _transactionService = transactionService;
        }
        #endregion

        [HttpGet]
        public IEnumerable<TransactionDTO> Get(int year, int month)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            if (userID == null)
            {
                return null;
            }

            var transactions = _transactionService.GetUserTransactions(userID, year, month);

            if (transactions == null)
            {
                return null;
            }

            var transactionsList = transactions.Select(t => new TransactionDTO() {
                Amount = t.Amount,
                CategoryName = t.Category.Name,
                ID = t.ID.ToString(),
                IsPlaned = t.IsPlaned,
                IsSpending = t.IsSpending,
                Name = t.Name,
                TransactionDate = t.TransactionDate
                });

            return transactionsList;
        }

        [HttpPut("SwitchPlaned/{id}")]
        public IActionResult SwitchPlaned(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _transactionService.ChangePlannedStatus(id);

            return Ok();
        }
    }
}
