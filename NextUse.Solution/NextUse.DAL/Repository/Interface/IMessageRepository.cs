using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllAsync();
        Task<Message?> GetByIdAsync(int id);
        Task<Message> AddAsync(Message newMessage);
        Task<Message> UpdateByIdAsync(int id, Message updatedMessage);
        Task DeleteByIdAsync(int id);
    }
}
