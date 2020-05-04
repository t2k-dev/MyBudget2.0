using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.API.Filters;
using MyBudget.API.Models.Transaction;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;

namespace MyBudget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        #region ctor & fields
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        #endregion

        [HttpGet]
        public IActionResult GetTransactions(string id, int year, int month)
        {
            try
            {
                var transactions = _transactionService.GetUserTransactions(id, year, month);

                if (transactions.Count() < 1)
                {
                    return NotFound();
                }

                return Ok(transactions);
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }

        [HttpPost]
        [CheckModelForNull]
        public async Task<IActionResult> Add([FromBody]TransactionCreateRequestDTO model)
        {
            try
            {
                var transaction = new TransactionModel()
                {
                    Name = model.Name,
                    Amount = model.Amount,
                    TransactionDate = model.TransDate,
                    CategoryID = model.CategoryId,
                    IsSpending = model.IsSpending,
                    IsPlaned = model.IsPlaned,
                    UserID = model.UserId,
                    CurrencyID = 1 // TODO: fix
                };

                var id = await _transactionService.AddTransactionAsync(transaction);

                return Created("", id.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [CheckModelForNull]
        public async Task<IActionResult> Edit(string id, [FromBody]TransactionUpdateRequestDTO model)
        {
            try
            {
                var transaction = _transactionService.GetTransaction(id);
                if (transaction == null)
                {
                    return NotFound();
                }

                transaction.Name = model.Name ?? transaction.Name;
                transaction.Amount = model.Amount ?? transaction.Amount;
                transaction.CategoryID = model.CategoryId;
                transaction.IsPlaned = model.IsPlaned ?? transaction.IsPlaned;
                transaction.TransactionDate = model.TransDate ?? transaction.TransactionDate;

                _transactionService.UpdateTransaction(transaction);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                _transactionService.DeleteTransaction(id);

                return Ok($"Transaction id = {id} is successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}