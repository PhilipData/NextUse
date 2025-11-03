using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using NextUse.Services.DTO.RatingDTO;
using NextUse.Services.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<RatingResponse> ratings = await _ratingService.GetAllAsync();

                if (ratings.Count() == 0)
                    return NoContent();

                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RatingRequest newRating)
        {
            try
            {
                return Ok(await _ratingService.AddAsync(newRating));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{ratingId}")]
        public async Task<IActionResult> FindbyId([FromRoute] int ratingId)
        {
            try
            {
                var ratingResponse = await _ratingService.GetByIdAsync(ratingId);

                if (ratingResponse == null)
                    return NotFound();

                return Ok(ratingResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("{ratingId}")]
        public async Task<IActionResult> UpdatedById([FromRoute] int ratingId, [FromBody] RatingRequest updatedRating)
        {
            try
            {
                var ratingResponse = await _ratingService.UpdateByIdAsync(ratingId, updatedRating);

                if (ratingResponse == null)
                {
                    return NotFound();
                }
                return Ok(ratingResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{ratingId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int ratingId)
        {
            try
            {
                await _ratingService.DeleteByIdAsync(ratingId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
