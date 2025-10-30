using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Data;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync() =>
            await _context.Ingredients.ToListAsync();

#pragma warning disable CS8603
        public async Task<Ingredient> GetByIdAsync(Guid id) =>
            await _context.Ingredients.FindAsync(id);
#pragma warning restore CS8603

        public async Task AddAsync(Ingredient ingredient) =>
            await _context.Ingredients.AddAsync(ingredient);

        public void Update(Ingredient ingredient) =>
            _context.Ingredients.Update(ingredient);

        public void Delete(Ingredient ingredient) =>
            _context.Ingredients.Remove(ingredient);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
