using NextUse.Service.DTO.ProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.Services.Interface
{
    public interface IProfileService
    {
        Task<IEnumerable<ProfileResponse>> GetAllAsync();
        Task<ProfileResponse?> GetByIdAsync(int id);
        Task<ProfileResponse?> GetByUserIdAsync(string userId);
        Task<ProfileResponse> AddAsync(ProfileRequest newProfileRequest);
        Task<ProfileResponse> UpdateByIdAsync(int id, ProfileRequest updatedProfileRequest);
        Task DeleteByIdAsync(int id);
    }
}
