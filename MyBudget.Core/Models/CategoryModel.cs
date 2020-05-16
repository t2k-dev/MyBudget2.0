using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Core.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Заполните поле")]
        [MaxLength(40,ErrorMessage = "Не длиннее 40 символов")]
        public string Name { get; set; }

        public bool IsSpendingCategory { get; set; }

        public string Icon { get; set; }

        public List<UserCategoryModel> UserCategories { get; set; }

        public string CreatedByID { get; set; }

        public bool IsSystem { get; set; }
    }
}
