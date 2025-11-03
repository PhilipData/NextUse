using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.AddressDTO
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public int PostalCode { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public AddressProfileResponse? Profile { get; set; }
        public AddressProductResponse Product { get; set; }
    }

    public class AddressProfileResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }

    }

    public class AddressProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
