using MyBudget.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBudget.Core.Attributes
{
    /// <summary>
    /// Compares total amount and current amount.
    /// </summary>
    public class GoalCurrentAmountValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (GoalModel)validationContext.ObjectInstance;
            if (Convert.ToDouble(value) > model.TotalAmount)
            {
                return new ValidationResult(FormatErrorMessage("CurrentAmount"));
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
