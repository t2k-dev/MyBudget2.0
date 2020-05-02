using MyBudget.Core.Models.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface IGraphService
    {
        public List<GraphItem> GetSpendingGraphByCategory(string userID, DateTime since, DateTime till);
    }
}
