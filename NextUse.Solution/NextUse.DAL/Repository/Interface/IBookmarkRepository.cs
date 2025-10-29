using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<Bookmark>> GetAllAsync();
        Task<IEnumerable<Bookmark>> GetByProfileIdAsync(int profileId);
        Task<Bookmark?> GetByIdAsync(int id);
        Task<Bookmark> AddAsync(Bookmark bookmark);
        Task<Bookmark> UpdateByIdAsync(int id, Bookmark updatedBookmark);
        Task DeleteByIdAsync(int id);
    }
}
