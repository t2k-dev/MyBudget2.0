using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.Web.Models.Transaction
{
    public class TransactionFormViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Domain.Transaction Transaction { get; set; }
    }
}
