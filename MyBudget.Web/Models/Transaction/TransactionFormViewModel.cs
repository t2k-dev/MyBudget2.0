using MyBudget.Core.Models;
using System.Collections.Generic;

namespace MyBudget.Web.Models.Transaction
{
    public class TransactionFormViewModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public TransactionModel Transaction { get; set; }
    }
}
