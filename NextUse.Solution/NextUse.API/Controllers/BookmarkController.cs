using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextUse.Services.DTO.BookmarkDTO;
using NextUse.Services.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var bookmarks = await _bookmarkService.GetAllAsync();
                if (!bookmarks.Any())
                {
                    return NoContent();
                }
                return Ok(bookmarks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var bookmark = await _bookmarkService.GetByIdAsync(id);
                if (bookmark == null)
                {
                    return NotFound();
                }
                return Ok(bookmark);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetByProfileId(int profileId)
        {
            try
            {
                var bookmarks = await _bookmarkService.GetByProfileIdAsync(profileId);
                if (!bookmarks.Any())
                {
                    return NoContent();
                }
                return Ok(bookmarks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookmarkRequest bookmarkRequest)
        {
            try
            {
                var bookmark = await _bookmarkService.AddAsync(bookmarkRequest);
                return CreatedAtAction(nameof(GetById), new { id = bookmark.Id }, bookmark);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] BookmarkRequest updatedBookmark)
        {
            try
            {
                var bookmark = await _bookmarkService.UpdateByIdAsync(id, updatedBookmark);
                if (bookmark == null)
                {
                    return NotFound();
                }
                return Ok(bookmark);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _bookmarkService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
