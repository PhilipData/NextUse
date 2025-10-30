using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.DTO.ProfileDTO
{
    public class ProfileRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 chars")]
        public required string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AddressId must not be 0")]
        public int AddressId { get; set; }
        
        public string UserId { get; set; }

    }
}
