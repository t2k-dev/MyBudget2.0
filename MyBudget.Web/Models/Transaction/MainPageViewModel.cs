using MyBudget.Core.Models;
using System.Collections.Generic;

namespace MyBudget.Web.Models.Transaction
{
    public class MainPageViewModel
    {
        /// <summary>
        /// Selected month date.
        /// </summary>
        public string ListDate { get; set; }

        /// <summary>
        /// User goals, debts and loans.
        /// </summary>
        public List<GoalModel> GoalsList { get; set; }

        /// <summary>
        /// User property DefaultCurrency.
        /// </summary>
        public string DefaultCurrency { get; set; }
    }
}
