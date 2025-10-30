// Repositories/UserRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Data;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _context.Users.ToListAsync();

#pragma warning disable CS8603
        public async Task<User> GetByIdAsync(Guid id) =>
            await _context.Users.FindAsync(id);
#pragma warning restore CS8603

        public async Task AddAsync(User user) =>
            await _context.Users.AddAsync(user);

        public void Update(User user) =>
            _context.Users.Update(user);

        public void Delete(User user) =>
            _context.Users.Remove(user);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public async Task<User> GetByUsernameAndPasswordHashAsync(string username, string passwordHash)
        {
#pragma warning disable CS8603
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == passwordHash);
#pragma warning restore CS8603

        }
    }
}
