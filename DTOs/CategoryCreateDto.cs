using System.ComponentModel.DataAnnotations;

namespace RecipeSharingAPI.DTOs
{
    public class CategoryCreateDto
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}