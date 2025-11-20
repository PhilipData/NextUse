using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface ICartRepository
    {
        Task<Cart?> GetOpenCartByProfileAsync(int profileId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
