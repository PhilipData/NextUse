using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.DTO.AddressDTO
{
    public class AddressRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 chars")]
        public required string Country { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 chars")]
        public required string City { get; set; }

        [Required]
        [Range(0, 999999, ErrorMessage = "PostalCode needs to be an integer between 0 and 999999")]
        public int PostalCode { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }

    }
}
