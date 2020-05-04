using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Transaction
{
    public class TransactionUpdateRequestDTO
    {
        public string Name { get; set; }
        public double? Amount { get; set; }
        public DateTime? TransDate { get; set; }
        public int CategoryId { get; set; }
        public bool? IsPlaned { get; set; }
    }
}
