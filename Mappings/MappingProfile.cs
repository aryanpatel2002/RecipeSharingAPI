using AutoMapper;
using RecipeSharingAPI.DTOs;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();

            // Recipe
            CreateMap<Recipe, RecipeReadDto>();
            CreateMap<RecipeCreateDto, Recipe>();

            // Ingredient
            CreateMap<Ingredient, IngredientReadDto>();
            CreateMap<IngredientCreateDto, Ingredient>();

            // Category
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();

            // RecipeIngredient
            CreateMap<RecipeIngredient, RecipeIngredientReadDto>();
            CreateMap<RecipeIngredientCreateDto, RecipeIngredient>();
        }
    }
}
