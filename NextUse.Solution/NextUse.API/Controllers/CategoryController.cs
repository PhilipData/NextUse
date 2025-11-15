using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextUse.Services.DTO.CategoryDTO;
using NextUse.Services.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<CategoryResponse> categories = await _categoryService.GetAllAsync();

                if (categories.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryRequest newCategory)
        {
            try
            {
                return Ok(await _categoryService.AddAsync(newCategory));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> FindbyId([FromRoute] int categoryId)
        {
            try
            {
                var categoryResponse = await _categoryService.GetByIdAsync(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{categoryId}")]
        public async Task<IActionResult> UpdatedById([FromRoute] int categoryId, [FromBody] CategoryRequest updatedCategory)
        {
            try
            {
                var categoryResponse = await _categoryService.UpdateByIdAsync(categoryId, updatedCategory);

                if (categoryResponse == null)
                {
                    return NotFound();
                }
                return Ok(categoryResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int categoryId)
        {
            try
            {
                await _categoryService.DeleteByIdAsync(categoryId);
                

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
