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
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDBContext _context;

        public ProfileRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _context.Profiles
                .Include(p => p.Address)
                .Include(p => p.Ratings)
                .Include(p => p.User)
                .Include(p => p.Bookmarks)
                    .ThenInclude(b => b.Product)
                .Include(p => p.Products)
                .ToListAsync();
        }
        public async Task<Profile?> GetByIdAsync(int id)
        {
            return await _context.Profiles
                .Include (p => p.Address)
                .Include(p => p.Ratings)
                .Include(p => p.User)
                .Include(p => p.Bookmarks)
                    .ThenInclude(b => b.Product)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Profile?> GetByUserIdAsync(string userId)
        {
            return await _context.Profiles
                .Include(p => p.Address)
                .Include(p => p.Ratings)
                .Include(p => p.User)
                .Include(p => p.Bookmarks)
                    .ThenInclude(b => b.Product)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Profile> AddAsync(Profile newProfile)
        {
            await _context.Profiles.AddAsync(newProfile);
            await _context.SaveChangesAsync();
            var profile = await GetByUserIdAsync(newProfile.UserId);

            if (profile is null)
                throw new Exception("Profile wasn't added");

            return profile;
        }

        public async Task<Profile> UpdateByIdAsync(int id, Profile updatedProfile)
        {
            var profile = await GetByIdAsync(id);

            if (profile is null)
                throw new Exception("Profile not found");

            profile.Name = updatedProfile.Name;
            profile.AddressId = updatedProfile.AddressId;

            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if(profile != null)
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> BlockByIdAsync(int profileId)
        {
            var profile = await _context.Profiles.FindAsync(profileId);
            if (profile == null || profile.IsBlocked) return false;

            profile.IsBlocked = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnblockByIdAsync(int profileId)
        {
            var profile = await _context.Profiles.FindAsync(profileId);
            if (profile == null || !profile.IsBlocked) return false;
            profile.IsBlocked = false;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
