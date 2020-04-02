namespace MyBudget.Core.Models
{
    public class TemplateModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public int Day { get; set; }

        public CategoryModel Category { get; set; }

        public int CategoryID { get; set; }

        public bool IsSpending { get; set; }

        public UserModel User { get; set; }

        public string UserID { get; set; }

        public CurrencyModel Currency { get; set; }

        public int CurrencyID { get; set; }

    }
}
