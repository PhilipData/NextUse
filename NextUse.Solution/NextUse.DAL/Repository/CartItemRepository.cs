using Microsoft.EntityFrameworkCore;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Extensions;
using NextUse.DAL.Repository.Interface;


namespace NextUse.DAL.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDBContext _context;
        public CartItemRepository(ApplicationDBContext context) => _context = context;

        public Task<CartItem?> GetByIdAsync(int id) =>
            _context.CartItems.Include(i => i.Product).FirstOrDefaultAsync(i => i.Id == id);

        public async Task AddAsync(CartItem item)
        {
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public void Remove(CartItem item) => _context.CartItems.Remove(item);

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
