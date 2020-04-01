using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.Web.Models.Account
{
    public class MangeViewModel
    {
        public int DefaultCurrencyID { get; set; }
        public bool CarryoverRests { get; set; }
    }
}
