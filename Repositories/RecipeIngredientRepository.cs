using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Data;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeIngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecipeIngredient>> GetAllAsync() =>
            await _context.RecipeIngredients.ToListAsync();

#pragma warning disable CS8603
        public async Task<RecipeIngredient> GetByIdAsync(Guid id) =>
            await _context.RecipeIngredients.FindAsync(id);
#pragma warning restore CS8603 

        public async Task AddAsync(RecipeIngredient recipeIngredient) =>
            await _context.RecipeIngredients.AddAsync(recipeIngredient);

        public void Update(RecipeIngredient recipeIngredient) =>
            _context.RecipeIngredients.Update(recipeIngredient);

        public void Delete(RecipeIngredient recipeIngredient) =>
            _context.RecipeIngredients.Remove(recipeIngredient);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
