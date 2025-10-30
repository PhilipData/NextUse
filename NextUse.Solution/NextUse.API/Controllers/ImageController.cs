using Microsoft.AspNetCore.Mvc;
using NextUse.Services.DTO.ImageDTO;
using NextUse.Services.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<ImageResponse> images = await _imageService.GetAllAsync();

                if (images.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(images);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ImageRequest newImage)
        {
            try
            {
                return Ok(await _imageService.AddRangeAsync(newImage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{imageId}")]
        public async Task<IActionResult> FindById([FromRoute] int imageId)
        {
            try
            {
                var imageResponse = await _imageService.GetByIdAsync(imageId);

                if (imageResponse == null)
                {
                    return NotFound();
                }

                return Ok(imageResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("product/{productId}")]
        public async Task<IActionResult> FindByProductId([FromRoute] int productId)
        {
            try
            {
                var imageResponse = await _imageService.GetByProductIdAsync(productId);

                if (imageResponse == null)
                {
                    return NotFound();
                }

                return Ok(imageResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{imageId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int imageId, [FromBody] ImageRequest updateImage)
        {
            try
            {
                var imageResponse = await _imageService.UpdateByIdAsync(imageId, updateImage);

                if (imageResponse == null)
                {
                    return NotFound();
                }
                return Ok(imageResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{imageId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int imageId)
        {
            try
            {
                await _imageService.DeleteByIdAsync(imageId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
