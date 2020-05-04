using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.API.Filters;
using MyBudget.API.Models.Goal;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;

namespace MyBudget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        #region ctor & fields

        private readonly IGoalService _goalService;

        public GoalsController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        #endregion

        public IActionResult GetGoals(string Id)
        {
            try
            {
                var goalsList = _goalService.GetUserGoals(Id);
                if (goalsList.Count() < 1)
                {
                    return NotFound();
                }

                var resultList = new List<GoalDTO>();
                // TODO add auto mapping
                foreach (var goal in goalsList)
                {
                    var goalDTO = new GoalDTO()
                    {
                        Id = goal.ID,
                        GoalName = goal.GoalName,
                        Type = goal.Type,
                        Amount = goal.TotalAmount,
                        CurAmount = goal.CurrentAmount,
                        IsActive = goal.IsActive,
                        UserId = goal.UserID,
                        CompleteDate = goal.CompleteDate
                    };
                    resultList.Add(goalDTO);
                }
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [CheckModelForNull]
        public async Task<IActionResult> Add([FromBody] GoalCreateRequestDTO model)
        {
            try
            {
                var goal = new GoalModel()
                {
                    GoalName = model.GoalName,
                    TotalAmount = model.Amount,
                    Type = model.Type,
                    UserID = model.UserId,
                    IsActive = true,
                    CurrentAmount = model.CurAmount ?? 0,
                    CompleteDate = model.CompleteDate ?? null,
                    CurrencyID = 1 // TODO
                };

                var id = await _goalService.AddGoalAsync(goal);

                return Created("", id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [CheckModelForNull]
        public IActionResult Edit(int id, [FromBody] GoalUpdateRequesDTO model)
        {
            try
            {
                var goal = _goalService.GetGoal(id);
                if (goal == null)
                {
                    return NotFound();
                }

                goal.GoalName = model.GoalName ?? goal.GoalName;
                goal.TotalAmount = model.Amount ?? goal.TotalAmount;
                goal.CurrentAmount = model.CurAmount.Value;
                goal.CompleteDate = model.CompleteDate;
                goal.IsActive = model.CurAmount == model.Amount ? false : true;

                _goalService.UpdateGoal(goal);
                
                return Ok($"Goal id = {id} is successfully modified");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _goalService.DeleteGoal(id);

                return Ok($"Goal id = {id} is successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/goals/{id}/payGoal")]
        public IActionResult PayGoal(int id, double amount)
        {
            // TODO Check for using.
            try
            {
                _goalService.PutMoney(amount, id);

                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}