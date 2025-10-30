// Repositories/CategoryRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Data;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();

#pragma warning disable CS8603
        public async Task<Category> GetByIdAsync(Guid id) =>
            await _context.Categories.FindAsync(id);
#pragma warning restore CS8603

        public async Task AddAsync(Category category) =>
            await _context.Categories.AddAsync(category);

        public void Update(Category category) =>
            _context.Categories.Update(category);

        public void Delete(Category category) =>
            _context.Categories.Remove(category);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
