using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating?> GetByIdAsync(int id);
        Task<Rating> AddAsync(Rating newRating);
        Task<Rating> UpdateByIdAsync(int id, Rating updatedRating);
        Task DeleteByIdAsync(int id);
        Task<bool> AlreadyRated(int fromProfileId, int toProfileId);
    }
}
