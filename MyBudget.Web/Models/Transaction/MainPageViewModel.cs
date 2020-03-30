using System.Collections.Generic;

namespace MyBudget.Web.Models.Transaction
{
    public class MainPageViewModel
    {
        /// <summary>
        /// Selected month date.
        /// </summary>
        public string ListDate { get; set; }

        public List<string> MyGoals { get; set; }

        /// <summary>
        /// User property DefaultCurrency.
        /// </summary>
        public string DefaultCurrency { get; set; }
    }
}
