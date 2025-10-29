using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.BookmarkDTO
{
    public class BookmarkRequest
    {
        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
