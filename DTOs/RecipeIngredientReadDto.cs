using System;

namespace RecipeSharingAPI.DTOs
{
    public class RecipeIngredientReadDto
    {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public float Quantity { get; set; }
        public string? Unit { get; set; }
        public string? Notes { get; set; }
    }
}