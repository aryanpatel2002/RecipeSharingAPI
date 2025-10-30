using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.DTOs;

namespace RecipeSharingAPI.Services
{
    public interface IRecipeIngredientService
    {
        Task<IEnumerable<RecipeIngredientReadDto>> GetAllRecipeIngredientsAsync();
        Task<RecipeIngredientReadDto> GetRecipeIngredientByIdAsync(Guid id);
        Task<RecipeIngredientReadDto> CreateRecipeIngredientAsync(RecipeIngredientCreateDto newRecipeIngredientDto);
        Task<bool> UpdateRecipeIngredientAsync(Guid id, RecipeIngredientCreateDto updatedRecipeIngredientDto);
        Task<bool> DeleteRecipeIngredientAsync(Guid id);
    }
}
