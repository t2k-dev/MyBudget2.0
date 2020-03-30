using System;

namespace MyBudget.Core.Models
{
    public class InitialCustomizationModel
    {
        public string UserID { get; set; }

        public string DefaultCurrency { get; set; }

        public bool CarryoverRests { get; set; }
    }
}
