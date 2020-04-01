using MyBudget.Core.Models;
using System.Collections.Generic;

namespace MyBudget.Core.Interfaces
{
    public interface IGoalService
    {
        public List<GoalModel> GetUserGoals(string userID);

        public GoalModel GetGoal(int goalID);

        public void AddGoal(GoalModel goal);

        public void UpdateGoal(GoalModel goal);

        public void PutMoney(double amount, int goalID);
    }
}
