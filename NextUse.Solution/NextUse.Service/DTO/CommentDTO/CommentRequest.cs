using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.CommentDTO
{
    public class CommentRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Comment cannot be longer than 100 chars")]
        public required string Content { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ProfileId must not be 0")]
        public int ProfileId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must not be 0")]
        public int ProductId { get; set; }
    }
}
