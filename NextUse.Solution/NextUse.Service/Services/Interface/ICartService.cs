using NextUse.Service.DTO.CartDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.Services.Interface
{
    public interface ICartService
    {
        Task<CartResponse> GetMyCartAsync(int profileId);
        Task<CartResponse> AddItemAsync(int profileId, int productId, int quantity);
        Task<CartResponse> UpdateItemAsync(int profileId, int cartItemId, int quantity);
        Task<CartResponse> RemoveItemAsync(int profileId, int cartItemId);
        Task<CartResponse> ClearAsync(int profileId);
        Task<CartResponse> CheckoutAsync(int profileId);
    }
}
