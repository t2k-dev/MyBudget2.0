using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Core.Interfaces;

namespace MyBudget.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class GraphsController : ControllerBase
    {
        #region ctor & fields

        private readonly IGraphService _graphService;

        public GraphsController(IGraphService graphService)
        {
            _graphService = graphService;
        }

        #endregion

        [Route("api/graph/getSpendingGraph/{userId}/{since}/{till}")]
        public IActionResult GetSpendingGraph(string userId, string since, string till)
        {
            try
            {
                var sinceParam = DateTime.ParseExact(since, "dd-MM-yyyy", null);
                var tillParam = DateTime.ParseExact(till, "dd-MM-yyyy", null);

                var resultList = _graphService.GetSpendingGraphByCategory(userId, sinceParam, tillParam);

                if (resultList.Count() < 1)
                {
                    return NotFound();
                }

                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}