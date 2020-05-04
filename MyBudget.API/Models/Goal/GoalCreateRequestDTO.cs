using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Goal
{
    public class GoalCreateRequestDTO
    {
        [Required]
        public string GoalName { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public byte Type { get; set; }
        [Required]
        public string UserId { get; set; }
        public double? CurAmount { get; set; }
        public DateTime? CompleteDate { get; set; }
        public bool IsActive { get; set; }
    }
}
