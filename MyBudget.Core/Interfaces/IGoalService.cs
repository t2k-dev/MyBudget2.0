using MyBudget.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface IGoalService
    {
        public List<Goal> GetUserGoals(string userID);

        public Goal GetGoal(int goalID);

        public void AddGoal(Goal goal);

        public void UpdateGoal(Goal goal);

        public void PutMoney(double amount, int goalID);
    }
}
