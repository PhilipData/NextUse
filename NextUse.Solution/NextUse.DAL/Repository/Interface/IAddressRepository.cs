using NextUse.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository.Interface
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(int id);
        Task<Address> AddAsync(Address newAddress);
        Task<Address> UpdateByIdAsync(int id, Address updatedAddress);
        Task DeleteByIdAsync(int id);
    }
}
