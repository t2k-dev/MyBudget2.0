using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;
using MyBudget.Web.Models.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<controller>
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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
