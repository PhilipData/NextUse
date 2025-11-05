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
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDBContext _context;

        public RatingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _context.Ratings.Include(p => p.FromProfile).Include(p => p.ToProfile).ToListAsync();
        }

        public async Task<Rating?> GetByIdAsync(int id)
        {
            return await _context.Ratings.Include(p => p.FromProfile).Include(p => p.ToProfile).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Rating> AddAsync(Rating newRating)
        {
            newRating.CreatedAt = DateTime.UtcNow; // Sets timestamp on creation
            await _context.Ratings.AddAsync(newRating);
            await _context.SaveChangesAsync();
            var rating = await GetByIdAsync(newRating.Id);

            if (rating is null)
                throw new Exception("Rating wasn't added");

            return rating;
        }

        public async Task<Rating> UpdateByIdAsync(int id, Rating updatedRating)
        {
            var rating = await GetByIdAsync(id);

            if (rating is null)
                throw new Exception("Rating not found");

            rating.Score = updatedRating.Score;

            await _context.SaveChangesAsync();
            return rating;
        }


        public async Task DeleteByIdAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
            else if (rating is null)
            {
                throw new Exception("Rating wasn't deleted");
            }
        }

        public async Task<bool> AlreadyRated(int fromProfileId, int toProfileId)
        {
            return await _context.Ratings.AnyAsync(r => r.FromProfileId == fromProfileId && r.ToProfileId == toProfileId);
        }



    }
}
