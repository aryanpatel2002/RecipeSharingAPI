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
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        /// <summary>
        /// Get all recipes.
        /// </summary>
        [HttpGet(Name = "GetAllRecipes")]
        [ProducesResponseType(typeof(IEnumerable<RecipeReadDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<RecipeReadDto>>> GetAllRecipes()
        {
            try
            {
                var recipes = await _recipeService.GetAllRecipesAsync();
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching recipes.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get a recipe by ID.
        /// </summary>
        [HttpGet("{id}", Name = "GetRecipeById")]
        [ProducesResponseType(typeof(RecipeReadDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RecipeReadDto>> GetRecipeById(Guid id)
        {
            try
            {
                var recipe = await _recipeService.GetRecipeByIdAsync(id);
                if (recipe == null)
                    return NotFound(new { message = $"Recipe with ID {id} not found." });

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving recipe.", details = ex.Message });
            }
        }

        /// <summary>
        /// Create a new recipe (Admin only).
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "CreateRecipe")]
        [ProducesResponseType(typeof(RecipeReadDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RecipeReadDto>> CreateRecipe([FromBody] RecipeCreateDto recipeCreateDto)
        {
            if (recipeCreateDto == null)
                return BadRequest("Recipe data cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdRecipe = await _recipeService.CreateRecipeAsync(recipeCreateDto);
                return CreatedAtRoute(nameof(GetRecipeById), new { id = createdRecipe.Id }, createdRecipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating recipe.", details = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing recipe (Admin only).
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}", Name = "UpdateRecipe")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateRecipe(Guid id, [FromBody] RecipeCreateDto recipeUpdateDto)
        {
            if (recipeUpdateDto == null)
                return BadRequest("Recipe data cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _recipeService.UpdateRecipeAsync(id, recipeUpdateDto);
                if (!result)
                    return NotFound(new { message = $"Recipe with ID {id} not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating recipe.", details = ex.Message });
            }
        }

        /// <summary>
        /// Delete a recipe by ID (Admin only).
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "DeleteRecipe")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteRecipe(Guid id)
        {
            try
            {
                var result = await _recipeService.DeleteRecipeAsync(id);
                if (!result)
                    return NotFound(new { message = $"Recipe with ID {id} not found." });

                return Ok(new { message = "Recipe deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting recipe.", details = ex.Message });
            }
        }
    }
}
