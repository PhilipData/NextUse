using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetByProfileIdAsync(int profileId);
        Task<Product?> GetByTitleAsync(string Title);
        Task<Product> AddAsync(Product newProduct);
        Task<Product> UpdateByIdAsync(int id, Product updatedProduct);
        Task DeleteByIdAsync(int id);
    }
}
