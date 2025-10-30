using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RecipeSharingAPI.DTOs;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Repositories;
using RecipeSharingAPI.Services;

namespace RecipeSharingAPI.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repo;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientReadDto>> GetAllIngredientsAsync()
        {
            var ingredients = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<IngredientReadDto>>(ingredients);
        }

#pragma warning disable CS8613
        public async Task<IngredientReadDto?> GetIngredientByIdAsync(Guid id)
#pragma warning restore CS8613
        {
            var ingredient = await _repo.GetByIdAsync(id);
            return ingredient == null ? null : _mapper.Map<IngredientReadDto>(ingredient);
        }

        public async Task<IngredientReadDto> CreateIngredientAsync(IngredientCreateDto newIngredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(newIngredientDto);
            ingredient.Id = Guid.NewGuid();

            await _repo.AddAsync(ingredient);
            await _repo.SaveChangesAsync();

            return _mapper.Map<IngredientReadDto>(ingredient);
        }

        public async Task<bool> UpdateIngredientAsync(Guid id, IngredientCreateDto updatedIngredientDto)
        {
            var existingIngredient = await _repo.GetByIdAsync(id);
            if (existingIngredient == null) return false;

            existingIngredient.Name = updatedIngredientDto.Name;
            existingIngredient.Description = updatedIngredientDto.Description;
            existingIngredient.IsAllergen = updatedIngredientDto.IsAllergen;
            existingIngredient.Unit = updatedIngredientDto.Unit;

            _repo.Update(existingIngredient);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteIngredientAsync(Guid id)
        {
            var existingIngredient = await _repo.GetByIdAsync(id);
            if (existingIngredient == null) return false;

            _repo.Delete(existingIngredient);
            return await _repo.SaveChangesAsync();
        }
    }
}
