using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Account
{
    public class ChangePasswordDTO
    {
        public string UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

    }
}
