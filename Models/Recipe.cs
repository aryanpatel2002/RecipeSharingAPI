using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSharingAPI.Models
{
    public class Recipe
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public byte[]? Image { get; set; }

        [ForeignKey("User")]
        public Guid? CreatedById { get; set; }

        public User? CreatedBy { get; set; }

        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }

        public ICollection<RecipeCategory>? RecipeCategories { get; set; }
    }
}