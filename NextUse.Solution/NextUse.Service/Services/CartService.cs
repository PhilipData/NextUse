using NextUse.Service.DTO.CartDTO;
using NextUse.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _carts;
        private readonly ICartItemRepository _items;
        private readonly IProductRepository _products;
        private readonly IProfileRepository _profiles;

        public CartService(
            ICartRepository carts,
            ICartItemRepository items,
            IProductRepository products,
            IProfileRepository profiles)
        {
            _carts = carts;
            _items = items;
            _products = products;
            _profiles = profiles;
        }

        public async Task<CartResponse> GetMyCartAsync(int profileId)
        {
            var cart = await EnsureOpenCart(profileId);
            return ToResponse(cart);
        }

        public async Task<CartResponse> AddItemAsync(int profileId, int productId, int quantity)
        {
            var cart = await EnsureOpenCart(profileId);

            var product = await _products.GetByIdAsync(productId)
                          ?? throw new KeyNotFoundException("Product not found.");

            var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
                cart.UpdatedAt = DateTime.UtcNow;
                await _carts.SaveChangesAsync();
                return ToResponse(cart);
            }

            var newItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = quantity,
                UnitPrice = product.Price
            };
            await _items.AddAsync(newItem);

            cart = (await _carts.GetByIdAsync(cart.Id))!;
            cart.UpdatedAt = DateTime.UtcNow;
            await _carts.SaveChangesAsync();

            return ToResponse(cart);
        }

        public async Task<CartResponse> UpdateItemAsync(int profileId, int cartItemId, int quantity)
        {
            var cart = await EnsureOpenCart(profileId);
            var item = cart.Items.FirstOrDefault(i => i.Id == cartItemId)
                       ?? throw new KeyNotFoundException("Cart item not found.");

            item.Quantity = quantity;
            cart.UpdatedAt = DateTime.UtcNow;
            await _carts.SaveChangesAsync();
            return ToResponse(cart);
        }

        public async Task<CartResponse> RemoveItemAsync(int profileId, int cartItemId)
        {
            var cart = await EnsureOpenCart(profileId);
            var item = cart.Items.FirstOrDefault(i => i.Id == cartItemId)
                       ?? throw new KeyNotFoundException("Cart item not found.");
            _items.Remove(item);
            cart.UpdatedAt = DateTime.UtcNow;
            await _items.SaveChangesAsync();
            return ToResponse((await _carts.GetByIdAsync(cart.Id))!);
        }

        public async Task<CartResponse> ClearAsync(int profileId)
        {
            var cart = await EnsureOpenCart(profileId);
            foreach (var i in cart.Items.ToList())
                _items.Remove(i);
            cart.UpdatedAt = DateTime.UtcNow;
            await _items.SaveChangesAsync();
            return ToResponse((await _carts.GetByIdAsync(cart.Id))!);
        }

        public async Task<CartResponse> CheckoutAsync(int profileId)
        {
            var cart = await EnsureOpenCart(profileId);
            
            cart.Status = "CheckedOut";
            cart.UpdatedAt = DateTime.UtcNow;
            await _carts.SaveChangesAsync();
            return ToResponse(cart);
        }

  
        private async Task<Cart> EnsureOpenCart(int profileId)
        {
            var profile = await _profiles.GetByIdAsync(profileId)
                          ?? throw new KeyNotFoundException("Profile not found");

            var cart = await _carts.GetOpenCartByProfileAsync(profileId);
            if (cart != null) return cart;

            return await _carts.CreateCartAsync(new Cart
            {
                ProfileId = profileId,
                Status = "Open"
            });
        }

        private static CartResponse ToResponse(Cart cart)
        {
            var resp = new CartResponse
            {
                Id = cart.Id,
                Status = cart.Status,
                CreatedAt = cart.CreatedAt.ToString("o"),
                UpdatedAt = cart.UpdatedAt.ToString("o"),
                Items = cart.Items.Select(i => new CartItemResponse
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Title = i.Product?.Title ?? "",
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
            resp.Total = resp.Items.Sum(x => x.UnitPrice * x.Quantity);
            return resp;
        }
    }
}
