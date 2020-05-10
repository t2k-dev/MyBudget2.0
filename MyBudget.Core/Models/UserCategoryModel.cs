using MyBudget.Core.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Models
{
    public class UserCategoryModel
    {
        public Guid UserID { get; set; }
        public UserModel User { get; set; }
        public int CategoryID { get; set; }
        public CategoryModel Category { get; set; }
    }
}
