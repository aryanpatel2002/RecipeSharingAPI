// Controllers/UserController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingAPI.DTOs;
using RecipeSharingAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeSharingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserReadDto>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdUser = await _userService.CreateUserAsync(userCreateDto);

            return CreatedAtRoute(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserCreateDto userUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.UpdateUserAsync(id, userUpdateDto);
            if (!result) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete user by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Toggle a user's role between Admin and User. Admins only.
        /// </summary>
        [HttpPost("{id}/toggle-role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserReadDto>> ToggleUserRole(Guid id)
        {
            var updated = await _userService.ToggleUserRoleAsync(id);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}
