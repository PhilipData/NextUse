using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllAsync();
        Task<Image?> GetByIdAsync(int id);
        Task<IEnumerable<Image>?> GetByProductIdAsync(int productId);
        Task<IEnumerable<Image>> AddRangeAsync(IEnumerable<Image> newImages);
        Task<Image> UpdateByIdAsync(int id, Image updatedImage);
        Task DeleteByIdAsync(int id);
    }
}
