using MyBudget.Core.Models.Account;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Core.Models
{
    public class TransactionModel
    {
        public Guid ID { get; set; }

        [MaxLength(90, ErrorMessage = "Не длиннее 90 символов")]
        public string Name { get; set; }

        [Range(1, 999999999, ErrorMessage = "Укажите корректную сумму")]
        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public CategoryModel Category { get; set; }

        public int CategoryID { get; set; }

        public bool IsSpending { get; set; }

        public string Description { get; set; }

        public bool IsPlaned { get; set; }

        public UserModel User { get; set; }

        public string UserID { get; set; }

        public CurrencyModel Currency { get; set; }

        public int CurrencyID { get; set; }
    }
}
