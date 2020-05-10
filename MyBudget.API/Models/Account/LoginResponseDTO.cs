using MyBudget.API.Models.Category;
using MyBudget.API.Models.Transaction;
using MyBudget.Core.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Account
{
    public class LoginResponseDTO
    {
        public int Status { get; set; }
        public UserConfigs UserSettings { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<TransactionListItem> Transactions { get; set; }
    }
}
