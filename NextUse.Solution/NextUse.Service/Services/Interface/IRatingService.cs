using NextUse.Services.DTO.RatingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.Services.Interface
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingResponse>> GetAllAsync();
        Task<RatingResponse?> GetByIdAsync(int id);
        Task<RatingResponse> AddAsync(RatingRequest newRatingRequest);
        Task<RatingResponse> UpdateByIdAsync(int id, RatingRequest updatedRatingRequest);
        Task DeleteByIdAsync(int id);
    }
}
