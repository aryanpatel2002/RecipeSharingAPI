using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeSharingAPI.DTOs
{
    public class RecipeIngredientCreateDto
    {
        [Required]
        public Guid RecipeId { get; set; }
        
        [Required]
        public Guid IngredientId { get; set; }
        
        public float Quantity { get; set; }
        
        [MaxLength(20)]
        public string? Unit { get; set; }
        
        public string? Notes { get; set; }
    }
}
