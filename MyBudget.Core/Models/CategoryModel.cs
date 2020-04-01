using System.Collections.Generic;

namespace MyBudget.Core.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool IsSpendingCategory { get; set; }

        public string Icon { get; set; }

        public List<UserCategoryModel> UserCategories { get; set; }

        public string CreatedByID { get; set; }

        public bool IsSystem { get; set; }
    }
}
