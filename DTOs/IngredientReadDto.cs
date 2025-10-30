using System;

namespace RecipeSharingAPI.DTOs
{
    public class IngredientReadDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsAllergen { get; set; }
        public string? Unit { get; set; }
    }
}