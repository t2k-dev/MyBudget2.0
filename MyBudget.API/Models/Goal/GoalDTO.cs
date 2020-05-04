using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Goal
{
    public class GoalDTO
    {
        public int Id { get; set; }

        public string GoalName { get; set; }

        public byte Type { get; set; }

        public double Amount { get; set; }

        public double CurAmount { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }

        public DateTime? CompleteDate { get; set; }
    }
}
