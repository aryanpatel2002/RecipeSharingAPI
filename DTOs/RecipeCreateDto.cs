using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeSharingAPI.DTOs
{
    public class RecipeCreateDto
    {
        [Required, MaxLength(100)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        // For simplicity, image can be base64 string
        public string? ImageBase64 { get; set; }

    }
}
