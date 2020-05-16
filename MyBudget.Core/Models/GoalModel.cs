using MyBudget.Core.Attributes;
using MyBudget.Core.Models.Account;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Core.Models
{
    public class GoalModel
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Заполните поле")]
        [StringLength(70,ErrorMessage = "Длинна поля не может быть больше 70 символов")]
        public string GoalName { get; set; }

        public byte Type { get; set; }

        [Range(0, 100000000)]
        [GoalCurrentAmountValidation(ErrorMessage = "Должна быть меньше полной суммы")]
        public double CurrentAmount { get; set; }

        [Range(1, 100000000, ErrorMessage = "Укажите сумму")]
        public double TotalAmount { get; set; }

        public bool IsActive { get; set; }

        public UserModel User { get; set; }

        public string UserID { get; set; }

        public DateTime? CompleteDate { get; set; }

        public string Description { get; set; }

        public CurrencyModel Currency { get; set; }

        public int CurrencyID { get; set; }
    }
}
