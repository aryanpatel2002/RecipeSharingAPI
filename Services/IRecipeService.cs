using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.DTOs;

namespace RecipeSharingAPI.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeReadDto>> GetAllRecipesAsync();
        Task<RecipeReadDto> GetRecipeByIdAsync(Guid id);
        Task<RecipeReadDto> CreateRecipeAsync(RecipeCreateDto newRecipeDto);
        Task<bool> UpdateRecipeAsync(Guid id, RecipeCreateDto updatedRecipeDto);
        Task<bool> DeleteRecipeAsync(Guid id);
    }
}
