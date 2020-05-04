using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBudget.API.Filters;
using MyBudget.API.Models.Template;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;

namespace MyBudget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        #region ctor & fields

        private readonly ITemplateService _templateService;

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        #endregion

        public IActionResult GetTemplates(string id)
        {
            try
            {
                var templates = _templateService.GetTemplates(id);
                if (templates.Count() < 1)
                {
                    return NotFound();
                }

                return Ok(templates);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [CheckModelForNull]
        public async Task<IActionResult> Add([FromBody]TemplateCreateRequestDTO model)
        {
            try
            {
                var template = new TemplateModel()
                {
                    Name = model.Name,
                    Amount = model.Amount,
                    Day = model.Day,
                    CategoryID = model.CategoryId,
                    IsSpending = model.IsSpending,
                    UserID = model.UserId,
                    CurrencyID = 1 // TODO
                };

                var id = await _templateService.AddTemplateAsync(template);

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
        public IActionResult Edit(int id, [FromBody]TemplateCreateRequestDTO model)
        {
            // TODO change TemplateCreateRequestDTO
            try
            {
                var template = _templateService.GetTemplate(id);
                if (template == null)
                {
                    return NotFound();
                }

                template.Name = model.Name;
                template.Amount = model.Amount;
                template.CategoryID = model.CategoryId;
                template.Day = model.Day;

                _templateService.UpdateTemplate(template);

                return Ok();
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
                _templateService.DeleteTemplate(id);

                return Ok($"Template id = {id} is successfully deleted");
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }
    }
}