using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextUse.Service.DTO.AddressDTO;
using NextUse.Service.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<AddressResponse> address = await _addressService.GetAllAsync();

                if (address.Count() == 0)
                    return NoContent();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressRequest newAddress)
        {
            try
            {
                return Ok(await _addressService.AddAsync(newAddress));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{addressId}")]
        public async Task<IActionResult> FindAddressById([FromRoute] int addressId)
        {
            try
            {
                var addressResponse = await _addressService.GetByIdAsync(addressId);

                if (addressResponse == null)
                {
                    return NotFound();
                }

                return Ok(addressResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{addressId}")]
        public async Task<IActionResult> UpdateAdressById([FromRoute] int addressId, [FromBody] AddressRequest updateAddress)
        {
            try
            {
                var addressResponse = await _addressService.UpdateByIdAsync(addressId, updateAddress);

                if (addressResponse == null)
                {
                    return NotFound();
                }
                return Ok(addressResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{addressId}")]
        public async Task<IActionResult> DeleteAddressById([FromRoute] int addressId)
        {
            try
            {
                await _addressService.DeleteByIdAsync(addressId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
