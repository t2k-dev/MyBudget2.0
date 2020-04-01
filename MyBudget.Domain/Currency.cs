using System.ComponentModel.DataAnnotations;

namespace MyBudget.Domain
{
    public class Currency
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(3)]
        public string Symbol { get; set; }
    }
}
