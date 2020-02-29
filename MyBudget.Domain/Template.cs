using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Domain
{
    public class Template
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public int Day { get; set; }

        public Category Category { get; set; }
        
        public int CategoryID { get; set; }

        public bool IsSpending { get; set; }

        public User User { get; set; }

        public string UserID { get; set; }
    }
}
