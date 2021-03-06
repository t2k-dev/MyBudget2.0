﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Domain
{
    public class Goal
    {
        public int ID { get; set; }

        [Required]
        [StringLength(70)]
        public string GoalName { get; set; }

        public byte Type { get; set; }
        
        public double CurrentAmount { get; set; }

        public double TotalAmount { get; set; }

        public bool IsActive { get; set; }

        public User User { get; set; }

        public string UserID { get; set; }

        public DateTime? CompleteDate { get; set; }

        public string Description { get; set; }

        public Currency Currency { get; set; }

        public int CurrencyID { get; set; }
    }
}
