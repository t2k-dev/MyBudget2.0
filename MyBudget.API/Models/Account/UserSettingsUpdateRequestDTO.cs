using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Account
{
    public class UserSettingsUpdateRequestDTO
    {
        public int? DefCurrency { get; set; }
        public bool? CarryoverRests { get; set; }
        public bool? UseTemplates { get; set; }
    }
}
