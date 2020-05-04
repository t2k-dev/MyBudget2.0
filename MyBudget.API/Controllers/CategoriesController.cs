using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.API.Models.Category;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;

namespace MyBudget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        #region ctor & fields

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        public IActionResult GetCategories(string userId)
        {
            try
            {
                var categories = _categoryService.GetAllUserCategories(userId);

                if (categories == null)
                {
                    return NotFound();
                }

                var categoryList = new List<CategoryDTO>();
                foreach (var item in categories)
                {
                    categoryList.Add(new CategoryDTO
                    {
                        Id = item.ID,
                        Name = item.Name,
                        IsSpendingCategory = item.IsSpendingCategory,
                        IsSystem = item.IsSystem,
                        CreatedBy = item.CreatedByID,
                        Icon = item.Icon
                    });

                }
                return Ok(categoryList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CategoryDTO model)
        {
            try
            {
                var category = new CategoryModel()
                {
                    Name = model.Name,
                    IsSpendingCategory = model.IsSpendingCategory,
                    CreatedByID = model.CreatedBy,
                    Icon = model.Icon
                };
                var id = await _categoryService.AddNewCategoryAsync(category);
                return Created("", id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, [FromBody]CategoryDTO model)
        {
            try
            {
                var category = _categoryService.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }

                category.Name = model.Name;
                category.Icon = model.Icon;

                _categoryService.UpdateCategory(category);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, string userId)
        {
            try
            {
                _categoryService.DeleteCategory(userId, id);

                return Ok($"Category with ID = {id} is successfully deleted for UserId = {userId}");
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