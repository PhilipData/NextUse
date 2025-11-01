using Microsoft.EntityFrameworkCore;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Extensions;
using NextUse.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(p => p.Products).ToListAsync();
        }

        public async Task<Category> AddAsync(Category newCategory)
        {
            _context.Categories.Add(newCategory);

            await _context.SaveChangesAsync();
            var category = await GetByIdAsync(newCategory.Id);

            if (category is null)
                throw new Exception("Category wasn't added");

            return category;
        }
        public async Task<Category?> GetByIdAsync(int categoryId)
        {
            return await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(c => c.Id == categoryId);
        }
        public async Task<Category?> UpdateByIdAsync(int categoryId, Category updateCategory)
        {
            var category = await GetByIdAsync(categoryId);

            if (category is null)
                throw new Exception("Category not found");

            category.Name = updateCategory.Name;

            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteByIdAsync(int categoryId)
        {
            var category = await GetByIdAsync(categoryId);

            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

    }
}
