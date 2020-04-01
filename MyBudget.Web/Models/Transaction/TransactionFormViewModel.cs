using MyBudget.Core.Models;
using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.Web.Models.Transaction
{
    public class TransactionFormViewModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public TransactionModel Transaction { get; set; }
    }
}
