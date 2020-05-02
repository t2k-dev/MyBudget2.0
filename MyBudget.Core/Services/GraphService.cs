using MyBudget.Core.Interfaces;
using MyBudget.Core.Models.Graph;
using MyBudget.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBudget.Core.Services
{
    public class GraphService: IGraphService
    {
        #region ctor & fields

        private readonly ApplicationContext _context;
        private readonly string[] _colors = {
                        "#ff3d67",
                        "#059bff",
                        "#ffc233",
                        "#CE93D8",
                        "#ff9124",
                        "#22cece",
                        "#7CB342",
                        "#FF5722",
                        "#D4E157",
                        "#26A69A",
                        "#9575CD",
                        "#81D4FA"
            };

        public GraphService(ApplicationContext context)
        {
            _context = context;
        }

        #endregion

        public List<GraphItem> GetSpendingGraphByCategory(string userID, DateTime since, DateTime till)
        {
            var categories = _context.Categories
                .Where(c => c.IsSpendingCategory == true
                        && c.UserCategories.Any(uc => uc.UserId == userID))
                .OrderBy(category => category.Name)
                .ToList();
            
            var transactions = _context.Transactions
                .Where(t => t.UserID == userID && t.TransactionDate >= since && t.TransactionDate <= till)
                .ToList();
            
            var resultGraphList = new List<GraphItem>();

            int i = 0;
            foreach (var cat in categories)
            {
                var item = new GraphItem();
                item.Amount = transactions.Where(t => t.CategoryID == cat.ID).Select(s => s.Amount).Sum();
                
                if (item.Amount > 0)
                {
                    item.Caption = cat.Name;
                    item.Color = _colors[i++];
                    resultGraphList.Add(item);
                }
            }

            return resultGraphList;
        }

    }
}
