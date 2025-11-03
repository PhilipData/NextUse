using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextUse.Services.DTO.ProfileDTO;
using NextUse.Services.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<ProfileResponse> profiles = await _profileService.GetAllAsync();

                if (profiles.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(profiles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProfileRequest newProfile)
        {
            try
            {
                return Ok(await _profileService.AddAsync(newProfile));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{profileId}")]
        public async Task<IActionResult> FindById([FromBody] int profileId)
        {
            try
            {
                var profileResponse = await _profileService.GetByIdAsync(profileId);

                if (profileResponse == null)
                    return NotFound();

                return Ok(profileResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{profileId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int profileId, [FromBody] ProfileRequest updateProfile)
        {
            try
            {
                var profileResponse = await _profileService.UpdateByIdAsync(profileId, updateProfile);

                if(profileResponse == null)
                    return NotFound();

                return Ok(profileResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{profileId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int profileId)
        {
            try
            {
                await _profileService.DeleteByIdAsync(profileId);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{profileId}/block")]
        public async Task<IActionResult> BlockById([FromRoute] int profileId)
        {
            try
            {
                bool result = await _profileService.BlockByIdAsync(profileId);

                if (!result)
                    return NotFound("Profile not found or already blocked.");

                return Ok("Profile blocked successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch]
        [Route("{profileId}/unblock")]
        public async Task<IActionResult> UnblockById([FromRoute] int profileId)
        {
            try
            {
                bool result = await _profileService.UnblockByIdAsync(profileId);

                if (!result)
                    return NotFound("Profile not found or already unblocked.");

                return Ok("Profile unblocked successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
