using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Data;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync() =>
            await _context.Recipes.Include(r => r.CreatedBy).ToListAsync();

#pragma warning disable CS8603
        public async Task<Recipe> GetByIdAsync(Guid id) =>
            await _context.Recipes.Include(r => r.CreatedBy)
                                   .FirstOrDefaultAsync(r => r.Id == id);
#pragma warning restore CS8603

        public async Task AddAsync(Recipe recipe) =>
            await _context.Recipes.AddAsync(recipe);

        public void Update(Recipe recipe) =>
            _context.Recipes.Update(recipe);

        public void Delete(Recipe recipe) =>
            _context.Recipes.Remove(recipe);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
