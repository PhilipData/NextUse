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
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(c => c.Profile).Include(c => c.Product).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(c => c.Profile).Include(c => c.Product).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> AddAsync(Comment newComment)
        {
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
            var comment = await GetByIdAsync(newComment.Id);

            if (comment is null)
                throw new Exception("Comment wasn't added");

            return comment;
        }

        public async Task<Comment> UpdateByIdAsync(int id, Comment updatedComment)
        {
            var existingComment = await GetByIdAsync(id);

            if (existingComment is null)
                throw new Exception("Comment not found");

            existingComment.Content = updatedComment.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment != null)
            {
                _context.Comments.Remove(existingComment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
