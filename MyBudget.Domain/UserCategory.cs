namespace MyBudget.Domain
{
    public class UserCategory
    {        
        public string UserId { get; set; }
        public User User { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
