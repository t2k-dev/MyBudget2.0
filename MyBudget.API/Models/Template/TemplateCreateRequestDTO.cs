using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Template
{
    public class TemplateCreateRequestDTO
    {
        [Required]
        public double Amount { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public bool IsSpending { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}
