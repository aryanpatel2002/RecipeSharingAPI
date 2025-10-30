using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string? Username { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public string? Role { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Recipe>? Recipes { get; set; }
    }
}