using NextUse.Services.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> AddAsync(CategoryRequest newCategory);
        Task<CategoryResponse> GetByIdAsync(int categoryId);
        Task<CategoryResponse> UpdateByIdAsync(int categoryId, CategoryRequest updateCategory);
        Task DeleteByIdAsync(int categoryId);
    }
}
