using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextUse.DAL.Database.Entities;
using NextUse.Services.DTO.ProductDTO;
using NextUse.Services.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productsService;
        public ProductController(IProductService products)
        {
            _productsService = products;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<ProductResponse> products = await _productsService.GetAllAsync();
                if (products.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductRequest product)
        {
            try
            {
                if (product != null)
                {
                    return Ok(await _productsService.AddAsync(product));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("full-product")]
        public async Task<IActionResult> AddProductWithImages([FromForm]ProductRequestWithImages product)
        {
            try
            {
                if (product != null)
                {
                    return Ok(await _productsService.AddWithImagesAsync(product));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<IActionResult> FindbyId([FromRoute] int productId)
        {
            try
            {
                var productResponse = await _productsService.GetByIdAsync(productId);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("profile/{profileId}")]
        public async Task<IActionResult> FindbyProfileId([FromRoute] int profileId)
        {
            try
            {
                var productResponse = await _productsService.GetByProfileIdAsync(profileId);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-title/{title}")]
        public async Task<IActionResult> FindbyId(string title)
        {
            try
            {
                var productResponse = await _productsService.GetByTitleAsync(title);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int productId, [FromBody] ProductRequest updatedProduct)
        {
            try
            {
                var productResponse = await _productsService.UpdateByIdAsync(productId, updatedProduct);
                if (updatedProduct == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int productId)
        {
            try
            {
                await _productsService.DeleteByIdAsync(productId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
