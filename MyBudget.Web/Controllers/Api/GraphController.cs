using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;

namespace MyBudget.Web.Controllers.Api
{
    public class GraphController : Controller
    {
        #region ctor & fields
        private readonly IGraphService _graphService;
        public GraphController(IGraphService graphService)
        {
            _graphService = graphService;
        }
        #endregion

        /// <summary>
        /// Get data for "Pie spending graph" current month only
        /// </summary>
        [Route("api/graph/getSpendingGraphCurrentMonth/{userId}")]
        public IActionResult GetSpendingGraphCurrentMonth(string userId)
        {
            try
            {
                var sinceParam = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var tillParam = sinceParam.AddMonths(1).AddDays(-1);
                var resultList = _graphService.GetSpendingGraphByCategory(userId, sinceParam, tillParam);

                if (resultList.Count() < 1)
                {
                    return NotFound();
                }

                return Ok(resultList);
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }

        /// <summary>
        /// Get data for "Pie spending graph"
        /// </summary>
        [Route("api/graph/getSpendingGraph/{userId}/{since}/{till}")]
        public IActionResult GetSpendingGraph(string userId, string since, string till)
        {
            try
            {
                var sinceParam = DateTime.ParseExact(since, "dd-MM-yyyy", null);
                var tillParam = DateTime.ParseExact(till, "dd-MM-yyyy", null);
                var resultList = _graphService.GetSpendingGraphByCategory(userId, sinceParam, tillParam); ;

                if (resultList.Count() < 1)
                {
                    return NotFound();
                }

                return Ok(resultList);
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }
    }
}