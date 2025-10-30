using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RecipeSharingAPI.DTOs;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Repositories;

namespace RecipeSharingAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repo;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeReadDto>> GetAllRecipesAsync()
        {
            var recipes = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RecipeReadDto>>(recipes);
        }

        public async Task<RecipeReadDto> GetRecipeByIdAsync(Guid id)
        {
            var recipe = await _repo.GetByIdAsync(id);
#pragma warning disable CS8603
            return recipe == null ? null : _mapper.Map<RecipeReadDto>(recipe);
#pragma warning restore CS8603
        }

        public async Task<RecipeReadDto> CreateRecipeAsync(RecipeCreateDto newRecipeDto)
        {
            var recipe = _mapper.Map<Recipe>(newRecipeDto);

            // Convert image base64 string to byte[]
            if (!string.IsNullOrEmpty(newRecipeDto.ImageBase64))
            {
                recipe.Image = Convert.FromBase64String(newRecipeDto.ImageBase64);
            }

            recipe.Id = Guid.NewGuid();

            await _repo.AddAsync(recipe);
            await _repo.SaveChangesAsync();

            return _mapper.Map<RecipeReadDto>(recipe);
        }

        public async Task<bool> UpdateRecipeAsync(Guid id, RecipeCreateDto updatedRecipeDto)
        {
            var existingRecipe = await _repo.GetByIdAsync(id);
            if (existingRecipe == null) return false;

            existingRecipe.Title = updatedRecipeDto.Title;
            existingRecipe.Description = updatedRecipeDto.Description;
            existingRecipe.CreationDate = updatedRecipeDto.CreationDate;

            if (!string.IsNullOrEmpty(updatedRecipeDto.ImageBase64))
            {
                existingRecipe.Image = Convert.FromBase64String(updatedRecipeDto.ImageBase64);
            }

            _repo.Update(existingRecipe);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteRecipeAsync(Guid id)
        {
            var existingRecipe = await _repo.GetByIdAsync(id);
            if (existingRecipe == null) return false;

            _repo.Delete(existingRecipe);
            return await _repo.SaveChangesAsync();
        }
    }
}
