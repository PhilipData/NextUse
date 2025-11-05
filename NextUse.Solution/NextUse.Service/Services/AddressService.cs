using NextUse.DAL.Database.Entities;
using NextUse.DAL.Repository.Interface;
using NextUse.Services.DTO.AddressDTO;
using NextUse.Services.Services.Interface;


namespace NextUse.Services.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public AddressResponse MapAddressToAddressResponse (Address address)
        {
            AddressResponse addressResponse = new AddressResponse
            {
                Id = address.Id,
                Country = address.Country,
                City = address.City,
                PostalCode = address.PostalCode,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
            };

            if (address.Profile != null)
            {
                addressResponse.Profile = new AddressProfileResponse
                {
                    Id = address.Profile.Id,
                    Name = address.Profile.Name,
                };
            }
            if (address.Product != null)
            {
                addressResponse.Product = new AddressProductResponse
                {
                    Id = address.Product.Id,
                    Title = address.Product.Title,
                    Description = address.Product.Description,
                    Price = address.Product.Price,
                };
            }

            return addressResponse;
        }

        public Address MapAddressRequestToAddress(AddressRequest addressRequest)
        {
            return new Address
            {
                Country = addressRequest.Country,
                City = addressRequest.City,
                PostalCode = addressRequest.PostalCode,
                Street = addressRequest.Street,
                HouseNumber = addressRequest.HouseNumber,
            };
        }

        public async Task<IEnumerable<AddressResponse>> GetAllAsync()
        {
            IEnumerable<Address> address = await _addressRepository.GetAllAsync();

            return address.Select(MapAddressToAddressResponse);
        }

        public async Task<AddressResponse?> GetByIdAsync(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);

            return address is null ? null : MapAddressToAddressResponse(address);
        }

        public async Task<AddressResponse> AddAsync(AddressRequest newAddressRequest)
        {
            var address = MapAddressRequestToAddress(newAddressRequest);

            var insertedAddress = await _addressRepository.AddAsync(address);

            return MapAddressToAddressResponse(insertedAddress);
        }

        public async Task<AddressResponse> UpdateByIdAsync(int id, AddressRequest updatedAddressRequest)
        {
            var address = MapAddressRequestToAddress(updatedAddressRequest);

            var updatedAddress = await _addressRepository.UpdateByIdAsync(id, address);

            return MapAddressToAddressResponse(updatedAddress);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _addressRepository.DeleteByIdAsync(id);
        }


    }
}
