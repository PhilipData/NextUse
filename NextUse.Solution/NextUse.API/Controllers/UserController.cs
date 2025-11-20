using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextUse.DAL.Extensions;
using NextUse.Services.DTO.AddressDTO;
using NextUse.Services.DTO.ProfileDTO;
using NextUse.Services.Services.Interface;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IAddressService _addressService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(
            IProfileService profileService,
            IAddressService addressService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _profileService = profileService;
            _addressService = addressService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                if (User.Identity is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                var claims = User.Claims.ToDictionary(x => x.Type, x => x.Value);

                return Ok(claims);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfileByUserId()
        {
            try
            {
                if (User.Identity is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim is null)
                    return BadRequest();

                var profile = await _profileService.GetByUserIdAsync(userIdClaim.Value);

                if (profile is null)
                    return NotFound();

                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

  


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid credentials." });
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, true, false);
                if (!result.Succeeded)
                {
                    return Unauthorized(new { message = "Invalid credentials." });
                }

                var profile = await _profileService.GetByUserIdAsync(user.Id);
                if (profile != null && profile.IsBlocked)
                {
                    
                    await _signInManager.SignOutAsync();
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "Your profile is blocked. Contact support." });
                }

                return Ok(new { message = "Login successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }




        [HttpPost("register")]
        public async Task<IActionResult> RegisterWithProfile(RegisterRequest registerRequest)
        {
            try
            {
                var user = new User
                {
                    UserName = registerRequest.Email,
                    Email = registerRequest.Email
                };

                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                var newlyCreatedUser = await _userManager.FindByEmailAsync(user.Email);

                if (newlyCreatedUser is null)
                    return BadRequest("Couldn't find new user");

                _userManager.AddClaimAsync(newlyCreatedUser, new Claim("level", "user")).GetAwaiter().GetResult();

                var address = await _addressService.AddAsync(new AddressRequest
                {
                    Country = registerRequest.Country,
                    City = registerRequest.City,
                    PostalCode = registerRequest.PostalCode,
                    Street = registerRequest.Street,
                    HouseNumber = registerRequest.HouseNumber,
                });

                var profile = await _profileService.AddAsync(new ProfileRequest
                {
                    Name = registerRequest.Name,
                    AddressId = address.Id,
                    UserId = newlyCreatedUser.Id,
                });

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserById(string userId)
        {

            return Ok();
        }


        public class LoginRequest
        {
            [Required]
            public required string Email { get; set; }

            [Required]
            public required string Password { get; set; }

        }

        public class LoginResponse
        {

        }

        public class RegisterRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public required string Name { get; set; }
            public required string Country { get; set; }
            public required string City { get; set; }
            public int PostalCode { get; set; }
            public string? Street { get; set; }
            public string? HouseNumber { get; set; }
        }
    }
}

