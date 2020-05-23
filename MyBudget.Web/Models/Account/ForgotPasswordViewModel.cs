using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Заполните поле")]
        public string UserNameOrEmail { get; set; }
    }
}
