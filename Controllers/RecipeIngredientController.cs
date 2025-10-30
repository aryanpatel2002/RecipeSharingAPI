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
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientService _service;

        public RecipeIngredientController(IRecipeIngredientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all recipe ingredients.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeIngredientReadDto>>> GetAll()
        {
            var items = await _service.GetAllRecipeIngredientsAsync();
            return Ok(items);
        }

        /// <summary>
        /// Get a recipe ingredient by ID.
        /// </summary>
        [HttpGet("{id}", Name = "GetRecipeIngredientById")]
        public async Task<ActionResult<RecipeIngredientReadDto>> GetById(Guid id)
        {
            var item = await _service.GetRecipeIngredientByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Create a new recipe ingredient.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RecipeIngredientReadDto>> Create([FromBody] RecipeIngredientCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newItem = await _service.CreateRecipeIngredientAsync(createDto);
            return CreatedAtRoute("GetRecipeIngredientById", new { id = newItem.Id }, newItem);
        }

        /// <summary>
        /// Update an existing recipe ingredient.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] RecipeIngredientCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateRecipeIngredientAsync(id, updateDto);
            if (!updated) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete a recipe ingredient by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteRecipeIngredientAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
