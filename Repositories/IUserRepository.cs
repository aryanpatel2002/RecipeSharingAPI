// Repositories/IUserRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task<bool> SaveChangesAsync();
        Task<User> GetByUsernameAndPasswordHashAsync(string username, string passwordHash);

    }
}
