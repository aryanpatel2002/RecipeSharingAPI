using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSharingAPI.Models
{
    public class RecipeIngredient
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Recipe")]
        public Guid RecipeId { get; set; }

        public Recipe? Recipe { get; set; }

        [ForeignKey("Ingredient")]
        public Guid IngredientId { get; set; }

        public Ingredient? Ingredient { get; set; }

        public float Quantity { get; set; }

        [MaxLength(20)]
        public string? Unit { get; set; }

        public string? Notes { get; set; }
    }
}