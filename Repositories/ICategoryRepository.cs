using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<bool> SaveChangesAsync();
    }
}
