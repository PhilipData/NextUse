using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category newCategory);
        Task<Category> GetByIdAsync(int categoryId);
        Task<Category> UpdateByIdAsync(int categoryId, Category updateCategory);
        Task DeleteByIdAsync(int categoryId);
    }
}
