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
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDBContext _context;

        public MessageRepository(ApplicationDBContext context)
        {
            _context = context;
        }




        public async Task<Message> AddAsync(Message newMessage)
        {
            await _context.Messages.AddAsync(newMessage);
            await  _context.SaveChangesAsync();
            var message = await GetByIdAsync(newMessage.Id);

            if (message is null)
                throw new Exception("Message wasn't added");

            return message;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var existingMessage = await _context.Messages.FindAsync(id);
            if (existingMessage != null)
            {
                _context.Messages.Remove(existingMessage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
           return await _context.Messages.Include(x=>x.FromProfile).Include(c => c.ToProfile). ToListAsync();
        }

        public async Task<Message?> GetByIdAsync(int id)
        {
            return await _context.Messages
                .Include(x => x.FromProfile).
                Include(c => c.ToProfile).
                FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Message> UpdateByIdAsync(int id, Message updatedMessage)
        {
            var existingMessage = await GetByIdAsync(id);

            if (existingMessage is null)
                throw new Exception("Message not found");

            existingMessage.Content = updatedMessage.Content;

            await _context.SaveChangesAsync();

            return existingMessage;
        }
    }
}
