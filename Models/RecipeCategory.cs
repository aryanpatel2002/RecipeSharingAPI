using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSharingAPI.Models
{
    public class RecipeCategory
    {
        [ForeignKey("Recipe")]
        public Guid RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}