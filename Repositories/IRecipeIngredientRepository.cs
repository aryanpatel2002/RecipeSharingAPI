using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public interface IRecipeIngredientRepository
    {
        Task<IEnumerable<RecipeIngredient>> GetAllAsync();
        Task<RecipeIngredient> GetByIdAsync(Guid id);
        Task AddAsync(RecipeIngredient recipeIngredient);
        void Update(RecipeIngredient recipeIngredient);
        void Delete(RecipeIngredient recipeIngredient);
        Task<bool> SaveChangesAsync();
    }
}
