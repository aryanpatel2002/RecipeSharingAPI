using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllAsync();
        Task<Ingredient> GetByIdAsync(Guid id);
        Task AddAsync(Ingredient ingredient);
        void Update(Ingredient ingredient);
        void Delete(Ingredient ingredient);
        Task<bool> SaveChangesAsync();
    }
}
