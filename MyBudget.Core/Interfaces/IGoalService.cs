using MyBudget.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudget.Core.Interfaces
{
    public interface IGoalService
    {
        public List<GoalModel> GetUserGoals(string userID);

        public GoalModel GetGoal(int goalID);

        public void AddGoal(GoalModel goal);

        public Task<int> AddGoalAsync(GoalModel goalModel);

        public void UpdateGoal(GoalModel goal);

        public void PutMoney(double amount, int goalID);

        public void DeleteGoal(int id);
    }
}
