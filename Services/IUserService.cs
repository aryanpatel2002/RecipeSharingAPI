// Services/IUserService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.DTOs;

namespace RecipeSharingAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(Guid id);
        Task<UserReadDto> CreateUserAsync(UserCreateDto newUserDto);
        Task<bool> UpdateUserAsync(Guid id, UserCreateDto updatedUserDto);
        Task<bool> DeleteUserAsync(Guid id);
        Task<UserReadDto> ValidateUserCredentialsAsync(string username, string password);
        Task<UserReadDto?> ToggleUserRoleAsync(Guid id);
    }
}
