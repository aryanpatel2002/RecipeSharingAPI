using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RecipeSharingAPI.DTOs;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Repositories;

namespace RecipeSharingAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _repo.GetByIdAsync(id);
#pragma warning disable CS8603
            return category == null ? null : _mapper.Map<CategoryReadDto>(category);
#pragma warning restore CS8603
        }

        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto newCategoryDto)
        {
            var category = _mapper.Map<Category>(newCategoryDto);
            category.Id = Guid.NewGuid();
            category.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(Guid id, CategoryCreateDto updatedCategoryDto)
        {
            var existingCategory = await _repo.GetByIdAsync(id);
            if (existingCategory == null) return false;

            existingCategory.Name = updatedCategoryDto.Name;
            existingCategory.Description = updatedCategoryDto.Description;
            existingCategory.IsActive = updatedCategoryDto.IsActive;

            _repo.Update(existingCategory);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await _repo.GetByIdAsync(id);
            if (existingCategory == null) return false;

            _repo.Delete(existingCategory);
            return await _repo.SaveChangesAsync();
        }
    }
}
