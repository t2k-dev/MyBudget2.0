﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Account
{
    public class ForgotPasswordRequestDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
