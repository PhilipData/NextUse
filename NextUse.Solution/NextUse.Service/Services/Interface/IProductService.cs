using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextUse.Services.DTO.ProductDTO;


namespace NextUse.Services.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<IEnumerable<ProductResponse>> GetByProfileIdAsync(int profileId);
        Task<ProductResponse?> GetByTitleAsync(string title);
        Task<ProductResponse> AddAsync(ProductRequest newProduct);
        Task<ProductResponse> AddWithImagesAsync(ProductRequestWithImages newProduct);
        Task<ProductResponse> UpdateByIdAsync(int id, ProductRequest updateProduct);
        Task DeleteByIdAsync(int id);
    }
}
