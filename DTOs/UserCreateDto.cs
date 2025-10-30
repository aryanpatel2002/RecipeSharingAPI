using System.ComponentModel.DataAnnotations;

namespace RecipeSharingAPI.DTOs
{
    public class UserCreateDto
    {
        [Required, MaxLength(50)]
        public string? Username { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        public string? Password { get; set; }  // Raw password, hash in Service
    }
}