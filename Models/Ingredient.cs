using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeSharingAPI.Models
{
    public class Ingredient
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsAllergen { get; set; } = false;

        [MaxLength(20)]
        public string? Unit { get; set; }  // e.g. grams, cups

        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}