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
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDBContext _context;

        public AddressRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Addresses.Include(a => a.Profile).ToListAsync();
        }
        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Addresses.Include(a => a.Profile).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Address> AddAsync(Address newAddress)
        {
            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();
            var address = await GetByIdAsync(newAddress.Id);

            if (address is null)
                throw new Exception("Address wasn't added");

            return address;

        }

        public async Task<Address> UpdateByIdAsync(int id, Address updatedAddress)
        {
            var existingAddress = await GetByIdAsync(id);

            if (existingAddress is null)
                throw new Exception("Address not found");


            existingAddress.Country = updatedAddress.Country;
            existingAddress.City = updatedAddress.City;
            existingAddress.PostalCode = updatedAddress.PostalCode;
            existingAddress.Street = updatedAddress.Street;
            existingAddress.HouseNumber = updatedAddress.HouseNumber;
            
            await _context.SaveChangesAsync();

            return existingAddress;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var existingAddress = await _context.Addresses.FindAsync(id);
            if(existingAddress != null)
            {
                _context.Addresses.Remove(existingAddress);
                await _context.SaveChangesAsync();
            }
        }
    }
}
