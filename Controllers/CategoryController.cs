using Microsoft.AspNetCore.Mvc;
using RecipeSharingAPI.DTOs;
using RecipeSharingAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        [HttpGet(Name = "GetAllCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryReadDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching categories.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get a category by ID.
        /// </summary>
        [HttpGet("{id}", Name = "GetCategoryById")]
        [ProducesResponseType(typeof(CategoryReadDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryById(Guid id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound(new { message = $"Category with ID {id} not found." });

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving category.", details = ex.Message });
            }
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType(typeof(CategoryReadDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CategoryReadDto>> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null)
                return BadRequest("Category data cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryCreateDto);
                return CreatedAtRoute(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating category.", details = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        [HttpPut("{id}", Name = "UpdateCategory")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateCategory(Guid id, [FromBody] CategoryCreateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null)
                return BadRequest("Category data cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
                if (!result)
                    return NotFound(new { message = $"Category with ID {id} not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating category.", details = ex.Message });
            }
        }

        /// <summary>
        /// Delete a category by ID.
        /// </summary>
        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (!result)
                    return NotFound(new { message = $"Category with ID {id} not found." });

                return Ok(new { message = "Category deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting category.", details = ex.Message });
            }
        }
    }
}
