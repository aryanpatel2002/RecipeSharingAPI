using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeSharingAPI.DTOs;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Repositories;

namespace RecipeSharingAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByIdAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto newUserDto)
        {
            var user = _mapper.Map<User>(newUserDto);
#pragma warning disable CS8604
            user.PasswordHash = HashPassword(newUserDto.Password);
#pragma warning restore CS8604
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserCreateDto updatedUserDto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            user.Username = updatedUserDto.Username;
            user.Email = updatedUserDto.Email;
#pragma warning disable CS8604
            user.PasswordHash = HashPassword(updatedUserDto.Password);
#pragma warning restore CS8604

            _repo.Update(user);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            _repo.Delete(user);
            return await _repo.SaveChangesAsync();
        }

#pragma warning disable CS8613
        public async Task<UserReadDto?> ValidateUserCredentialsAsync(string username, string password)
#pragma warning restore CS8613
        {
            // Hash incoming password
            string hashedPassword = HashPassword(password);

            // Find user by username and hashed password
            var userEntity = await _repo.GetByUsernameAndPasswordHashAsync(username, hashedPassword);
            if (userEntity == null)
            {
                return null;
            }

            return _mapper.Map<UserReadDto>(userEntity);
        }

        public async Task<UserReadDto?> ToggleUserRoleAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;

            // Normalize roles and toggle between Admin and User
            var current = (user.Role ?? "").Trim();
            user.Role = string.Equals(current, "Admin", StringComparison.OrdinalIgnoreCase) ? "User" : "Admin";

            _repo.Update(user);
            var saved = await _repo.SaveChangesAsync();
            if (!saved) return null;

            return _mapper.Map<UserReadDto>(user);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}