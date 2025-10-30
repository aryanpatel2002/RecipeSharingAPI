using Microsoft.AspNetCore.Mvc;
using RecipeSharingAPI.Services;
using RecipeSharingAPI.DTOs;
using System.Threading.Tasks;

namespace RecipeSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IUserService _userService;

        public AuthController(JwtTokenService jwtTokenService, IUserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }

        public class LoginDto
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        // ---------------- LOGIN ----------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Username and password are required.");

            var user = await _userService.ValidateUserCredentialsAsync(loginDto.Username, loginDto.Password);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            // ✅ Generate JWT token with Role claim
            var token = _jwtTokenService.GenerateToken(user.Id.ToString(), user.Role);

            // ✅ Return role + username for frontend
            return Ok(new
            {
                Token = token,
                Username = user.Username,
                Role = user.Role,
                Message = $"Welcome back, {user.Username}!"
            });
        }

        // ---------------- REGISTER ----------------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto newUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _userService.CreateUserAsync(newUserDto);

            return CreatedAtRoute("GetUserById", new { id = createdUser.Id }, new
            {
                createdUser.Id,
                createdUser.Username,
                createdUser.Email,
                createdUser.Role
            });
        }
    }
}