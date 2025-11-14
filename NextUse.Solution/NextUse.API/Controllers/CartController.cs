using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextUse.Service.DTO.CartDTO;
using NextUse.Service.Services.Interface;
using NextUse.Services.Services.Interface;
using System.Security.Claims;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IProfileService _profileService;

        public CartController(ICartService cartService, IProfileService profileService)
        {
            _cartService = cartService;
            _profileService = profileService;
        }

        private async Task<int> GetCurrentProfileIdAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? throw new UnauthorizedAccessException("No user id");
            var profile = await _profileService.GetByUserIdAsync(userId);
            if (profile == null) throw new Exception("Profile not found");
            return profile.Id;
        }

        [HttpGet]
        public async Task<ActionResult<CartResponse>> GetMyCart()
        {
            var profileId = await GetCurrentProfileIdAsync();
            var result = await _cartService.GetMyCartAsync(profileId);
            return Ok(result);
        }

        [HttpPost("items")]
        public async Task<ActionResult<CartResponse>> AddItem([FromBody] AddCartItemRequest req)
        {
            var profileId = await GetCurrentProfileIdAsync();
            var result = await _cartService.AddItemAsync(profileId, req.ProductId, req.Quantity);
            return Ok(result);
        }

        [HttpPut("items")]
        public async Task<ActionResult<CartResponse>> UpdateItem([FromBody] UpdateCartItemRequest req)
        {
            var profileId = await GetCurrentProfileIdAsync();
            var result = await _cartService.UpdateItemAsync(profileId, req.CartItemId, req.Quantity);
            return Ok(result);
        }

        [HttpDelete("items/{cartItemId:int}")]
        public async Task<ActionResult<CartResponse>> RemoveItem(int cartItemId)
        {
            var profileId = await GetCurrentProfileIdAsync();
            var result = await _cartService.RemoveItemAsync(profileId, cartItemId);
            return Ok(result);
        }

        [HttpDelete("clear")]
        public async Task<ActionResult<CartResponse>> Clear()
        {
            var profileId = await GetCurrentProfileIdAsync();
            var result = await _cartService.ClearAsync(profileId);
            return Ok(result);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CartResponse>> Checkout()
        {
            var profileId = await GetCurrentProfileIdAsync();
            var result = await _cartService.CheckoutAsync(profileId);
            return Ok(result);
        }
    }
}
