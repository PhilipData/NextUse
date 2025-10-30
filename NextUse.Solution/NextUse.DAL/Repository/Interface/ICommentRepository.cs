using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> AddAsync(Comment newComment);
        Task<Comment> UpdateByIdAsync(int id, Comment updatedComment);
        Task DeleteByIdAsync(int id);
    }
}
