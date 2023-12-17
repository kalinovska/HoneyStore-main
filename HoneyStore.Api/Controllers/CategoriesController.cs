using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoneyStore.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null)
            {
                return NoContent();
            }

            return Ok(categories);
        }


        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                await _categoryService.AddCategoryAsync(category);

                return CreatedAtRoute("CategoryById", new {id = category.Id}, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                var categoryFromDb = await _categoryService.GetCategoryAsync(id);

                if (categoryFromDb == null)
                {
                    return NotFound();
                }

                await _categoryService.UpdateCategoryAsync(id, category);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryFromDb = await _categoryService.GetCategoryAsync(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            await _categoryService.RemoveCategoryAsync(id);
            return Ok();
        }
    }
}
