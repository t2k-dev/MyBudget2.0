using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Account
{
    public class LoginRequestDTO
    {
        [Required]
        public string usr { get; set; }
        [Required]
        public string pass { get; set; }

    }
}
