using Microsoft.AspNetCore.Authorization;
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
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        /// <summary>
        /// Get all ingredients.
        /// </summary>
        [HttpGet(Name = "GetAllIngredients")]
        [ProducesResponseType(typeof(IEnumerable<IngredientReadDto>), 200)]
        // [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<IngredientReadDto>>> GetAllIngredients()
        {
            try
            {
                var ingredients = await _ingredientService.GetAllIngredientsAsync();
                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching ingredients.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get ingredient by ID.
        /// </summary>
        [HttpGet("{id}", Name = "GetIngredientById")]
        [ProducesResponseType(typeof(IngredientReadDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IngredientReadDto>> GetIngredientById(Guid id)
        {
            try
            {
                var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
                if (ingredient == null)
                    return NotFound(new { message = $"Ingredient with ID {id} not found." });

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving ingredient.", details = ex.Message });
            }
        }

        /// <summary>
        /// Create a new ingredient (Admin only).
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "CreateIngredient")]
        [ProducesResponseType(typeof(IngredientReadDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IngredientReadDto>> CreateIngredient([FromBody] IngredientCreateDto ingredientCreateDto)
        {
            if (ingredientCreateDto == null)
                return BadRequest("Ingredient data cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdIngredient = await _ingredientService.CreateIngredientAsync(ingredientCreateDto);
                return CreatedAtRoute(nameof(GetIngredientById), new { id = createdIngredient.Id }, createdIngredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating ingredient.", details = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing ingredient (Admin only).
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}", Name = "UpdateIngredient")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateIngredient(Guid id, [FromBody] IngredientCreateDto ingredientUpdateDto)
        {
            if (ingredientUpdateDto == null)
                return BadRequest("Ingredient data cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _ingredientService.UpdateIngredientAsync(id, ingredientUpdateDto);
                if (!result)
                    return NotFound(new { message = $"Ingredient with ID {id} not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating ingredient.", details = ex.Message });
            }
        }

        /// <summary>
        /// Delete ingredient by ID (Admin only).
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "DeleteIngredient")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteIngredient(Guid id)
        {
            try
            {
                var result = await _ingredientService.DeleteIngredientAsync(id);
                if (!result)
                    return NotFound(new { message = $"Ingredient with ID {id} not found." });

                return Ok(new { message = "Ingredient deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting ingredient.", details = ex.Message });
            }
        }
    }
}
