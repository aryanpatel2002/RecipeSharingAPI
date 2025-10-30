using System;
using System.Collections.Generic;

namespace RecipeSharingAPI.DTOs
{
    public class RecipeReadDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[]? Image { get; set; }
        public Guid CreatedById { get; set; }
        public string? CreatedByUsername { get; set; }
    }
}