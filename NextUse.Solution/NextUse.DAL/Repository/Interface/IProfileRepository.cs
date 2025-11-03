using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAllAsync();
        Task<Profile?> GetByIdAsync(int id);
        Task<Profile?> GetByUserIdAsync(string userId);
        Task<Profile> AddAsync(Profile newProfile);
        Task<Profile> UpdateByIdAsync(int id, Profile updatedProfile);
        Task DeleteByIdAsync(int id);

        Task<bool> BlockByIdAsync(int profileId);
        Task<bool> UnblockByIdAsync(int profileId);
    }
}
