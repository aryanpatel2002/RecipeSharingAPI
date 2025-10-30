using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeSharingAPI.DTOs;

namespace RecipeSharingAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();
        Task<CategoryReadDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto newCategoryDto);
        Task<bool> UpdateCategoryAsync(Guid id, CategoryCreateDto updatedCategoryDto);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
