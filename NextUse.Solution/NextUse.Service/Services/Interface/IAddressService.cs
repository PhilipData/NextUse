using NextUse.Service.DTO.AddressDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.Services.Interface
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressResponse>> GetAllAsync();
        Task<AddressResponse?> GetByIdAsync(int id);
        Task<AddressResponse> AddAsync(AddressRequest newAddressRequest);
        Task<AddressResponse> UpdateByIdAsync(int id, AddressRequest updatedAddressRequest);
        Task DeleteByIdAsync(int id);
    }
}
