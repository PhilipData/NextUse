using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.DTO.CartDTO
{
    public class AddCartItemRequest
    {
        [Required]
        public int ProductId { get; set; }
        
        [Range(1, int.MaxValue)] 
        public int Quantity { get; set; } = 1;
    }

    public class UpdateCartItemRequest
    {
        [Required]
        public int CartItemId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
