using NextUse.DAL.Database.Entities;
using NextUse.DAL.Repository.Interface;
using NextUse.Service.DTO.CategoryDTO;
using NextUse.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private CategoryResponse MapCategoryToCategoryResponse(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(prod => new CategoryProductResponse 
                {
                    Id = prod.Id,
                    Title = prod.Title,
                    Description = prod.Description,
                    Price = prod.Price
                }).ToList()
            };
        }
        private Category MapCategoryRequestToCategory(CategoryRequest newCategory)
        {
            return new Category
            {
                Name = newCategory.Name,
            };
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();
            
            return categories.Select(MapCategoryToCategoryResponse).ToList();
        }

        public async Task<CategoryResponse?> GetByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            return category is null ? null : MapCategoryToCategoryResponse(category);
        }

        public async Task<CategoryResponse> AddAsync(CategoryRequest newCategoryRequest)
        {
            var category = MapCategoryRequestToCategory(newCategoryRequest);
            
            var insertedCategory = await _categoryRepository.AddAsync(category);
            
            return MapCategoryToCategoryResponse(insertedCategory);
        }

        public async Task<CategoryResponse> UpdateByIdAsync(int categoryId, CategoryRequest updatedCategoryRequest)
        {
            var category = MapCategoryRequestToCategory(updatedCategoryRequest);
            
            var updatedCategory = await _categoryRepository.UpdateByIdAsync(categoryId, category);
            
            return MapCategoryToCategoryResponse(updatedCategory);
        }


        public async Task DeleteByIdAsync(int categoryId)
        {
            await _categoryRepository.DeleteByIdAsync(categoryId);
        }

    }
}
