using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.DTOs;

namespace RecipeSharingAPI.Services
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientReadDto>> GetAllIngredientsAsync();
        Task<IngredientReadDto> GetIngredientByIdAsync(Guid id);
        Task<IngredientReadDto> CreateIngredientAsync(IngredientCreateDto newIngredientDto);
        Task<bool> UpdateIngredientAsync(Guid id, IngredientCreateDto updatedIngredientDto);
        Task<bool> DeleteIngredientAsync(Guid id);
    }
}
