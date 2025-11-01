using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.MessageDTO
{
    public class MessageRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Comment cannot be longer than 100 chars")]
        public required string Content { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "FromProfileId must not be 0")]
        public int FromProfileId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ToProfileId must not be 0")]
        public int ToProfileId{ get; set; }
    }
}
