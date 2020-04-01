using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MyBudget.Domain
{
    public class User : IdentityUser
    {        
        public int DefaultCurrencyID { get; set; }
        public Currency DefaultCurrency { get; set; }
        public bool CarryoverRests { get; set; }
        public bool UseTemplates { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<UserCategory> UserCategories { get; set; }

        public User()
        {
            //Categories = new List<Category>();
        }
    }
}
