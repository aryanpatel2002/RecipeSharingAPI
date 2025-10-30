// DTOs/IngredientCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace RecipeSharingAPI.DTOs
{
    public class IngredientCreateDto
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsAllergen { get; set; } = false;

        [MaxLength(20)]
        public string? Unit { get; set; }
    }
}
