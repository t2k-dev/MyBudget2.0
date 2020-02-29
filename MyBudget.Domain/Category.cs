using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Domain
{
    public class Category
    {
        public int ID { get; set; }

        public string Name { get; set; }
         
        public bool IsSpendingCategory { get; set; }

        public string Icon { get; set; }

        public List<UserCategory> UserCategories { get; set; }

        public string CreatedByID { get; set; }

        public bool IsSystem { get; set; }

        public Category()
        {
            //Users = new List<User>();
        }

        #region System Categories

        /// <summary>
        /// "Без Категории" for income transactions Id = 1
        /// </summary>
        public static readonly int IncomeCategoryDefault = 1;

        /// <summary>
        /// "Без Категории" for spending transactions Id = 2
        /// </summary>
        public static readonly int SpendingCategoryDefault = 2;

        /// <summary>
        /// Take money from somebody
        /// </summary>
        public static readonly int TakeDebt = 3;

        /// <summary>
        /// Give money to somebody
        /// </summary>
        public static readonly int GiveCredit = 4;

        /// <summary>
        /// Rest from the previous month
        /// </summary>
        public static readonly int Rest = 5;

        /// <summary>
        /// Give borrowed money
        /// </summary>
        public static readonly int PayCredit = 6;

        /// <summary>
        ///  Return given money
        /// </summary>
        public static readonly int RecieveDebt = 7;

        /// <summary>
        /// Put money for a Goal
        /// </summary>
        public static readonly int PayGoal = 8;


        #endregion
    }
}

