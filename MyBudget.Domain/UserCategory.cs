using System;

namespace MyBudget.Domain
{
    public class UserCategory
    {        
        public Guid UserID { get; set; }
        public User User { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
