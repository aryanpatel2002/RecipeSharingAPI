using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RecipeSharingAPI.DTOs;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Repositories;

namespace RecipeSharingAPI.Services
{
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly IRecipeIngredientRepository _repo;
        private readonly IMapper _mapper;

        public RecipeIngredientService(IRecipeIngredientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeIngredientReadDto>> GetAllRecipeIngredientsAsync()
        {
            var recipeIngredients = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RecipeIngredientReadDto>>(recipeIngredients);
        }

        public async Task<RecipeIngredientReadDto> GetRecipeIngredientByIdAsync(Guid id)
        {
            var recipeIngredient = await _repo.GetByIdAsync(id);
#pragma warning disable CS8603
            return recipeIngredient == null ? null : _mapper.Map<RecipeIngredientReadDto>(recipeIngredient);
#pragma warning restore CS8603
        }

        public async Task<RecipeIngredientReadDto> CreateRecipeIngredientAsync(RecipeIngredientCreateDto newRecipeIngredientDto)
        {
            var recipeIngredient = _mapper.Map<RecipeIngredient>(newRecipeIngredientDto);
            recipeIngredient.Id = Guid.NewGuid();

            await _repo.AddAsync(recipeIngredient);
            await _repo.SaveChangesAsync();

            return _mapper.Map<RecipeIngredientReadDto>(recipeIngredient);
        }

        public async Task<bool> UpdateRecipeIngredientAsync(Guid id, RecipeIngredientCreateDto updatedRecipeIngredientDto)
        {
            var existingRecipeIngredient = await _repo.GetByIdAsync(id);
            if (existingRecipeIngredient == null) return false;

            existingRecipeIngredient.RecipeId = updatedRecipeIngredientDto.RecipeId;
            existingRecipeIngredient.IngredientId = updatedRecipeIngredientDto.IngredientId;
            existingRecipeIngredient.Quantity = updatedRecipeIngredientDto.Quantity;
            existingRecipeIngredient.Unit = updatedRecipeIngredientDto.Unit;
            existingRecipeIngredient.Notes = updatedRecipeIngredientDto.Notes;

            _repo.Update(existingRecipeIngredient);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteRecipeIngredientAsync(Guid id)
        {
            var existingRecipeIngredient = await _repo.GetByIdAsync(id);
            if (existingRecipeIngredient == null) return false;

            _repo.Delete(existingRecipeIngredient);
            return await _repo.SaveChangesAsync();
        }
    }
}
