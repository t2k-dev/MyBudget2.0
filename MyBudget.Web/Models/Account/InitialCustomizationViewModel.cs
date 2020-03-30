using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.Web.Models.Account
{
    public class InitialCustomizationViewModel
    {
        public string DefaultCurrency { get; set; }

        public bool CarryoverRests { get; set; }
    }
}
