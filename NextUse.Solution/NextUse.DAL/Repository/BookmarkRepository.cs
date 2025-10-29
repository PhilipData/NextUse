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
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly ApplicationDBContext _context;

        public BookmarkRepository(ApplicationDBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Bookmark>> GetAllAsync()
        {
            return await _context.Bookmarks
                .Include(b => b.Profile)
                .Include(b => b.Product)
                .ToListAsync();
        }

        public async Task<Bookmark?> GetByIdAsync(int id)
        {
            return await _context.Bookmarks
                .Include(b => b.Profile)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Bookmark>> GetByProfileIdAsync(int profileId)
        {
            return await _context.Bookmarks
                .Include(b => b.Profile)
                .Include(b => b.Product)
                .Where(b => b.ProfileId == profileId)
                .ToListAsync();
        }

        public async Task<Bookmark> AddAsync(Bookmark newBookmark)
        {
            await _context.Bookmarks.AddAsync(newBookmark);
            await _context.SaveChangesAsync();
            var bookmark = await GetByIdAsync(newBookmark.Id);

            if (bookmark is null)
                throw new Exception("Bookmark wasn't added");

            return bookmark;
        }

        public async Task<Bookmark> UpdateByIdAsync(int id, Bookmark updatedBookmark)
        {
            var existingBookmark = await GetByIdAsync(id);

            if (existingBookmark is null)
                throw new Exception("Bookmark not found");

            existingBookmark.ProfileId = updatedBookmark.ProfileId;
            existingBookmark.ProductId = updatedBookmark.ProductId;

            await _context.SaveChangesAsync();
            return existingBookmark;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var existingBookmark = await _context.Bookmarks.FindAsync(id);
            if (existingBookmark != null)
            {
                _context.Bookmarks.Remove(existingBookmark);
                await _context.SaveChangesAsync();
            }
        }
    }
}
