using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<RecipeCategory>? RecipeCategories { get; set; }
    }
}
