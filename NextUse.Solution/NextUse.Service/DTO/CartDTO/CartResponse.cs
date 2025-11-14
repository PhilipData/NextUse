using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.DTO.CartDTO
{
    public class CartResponse
    {
        public int Id { get; set; }
        public string Status { get; set; } = "Active";
        public string CreatedAt { get; set; } = "";
        public string UpdatedAt { get; set; } = "";
        public decimal Total { get; set; }
        public List<CartItemResponse> Items { get; set; } = new();
    }

    public class CartItemResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string Title { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => UnitPrice * Quantity;
    }
}
