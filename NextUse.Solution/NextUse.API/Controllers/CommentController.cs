using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextUse.Services.DTO.CommentDTO;
using NextUse.Services.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await _commentService.GetAllAsync();
                if (comments.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CommentRequest newComment)
        {
            try
            {
                return Ok(await _commentService.AddAsync(newComment));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> FindById([FromRoute] int commentId)
        {
            try
            {
                var commentResponse = await _commentService.GetByIdAsync(commentId);

                if (commentResponse == null)
                {
                    return NotFound();
                }

                return Ok(commentResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int commentId, [FromBody] CommentRequest updateComment)
        {
            try
            {
                var commentResponse = await _commentService.UpdateByIdAsync(commentId, updateComment);

                if (commentResponse == null)
                {
                    return NotFound();
                }

                return Ok(commentResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int commentId)
        {
            try
            {
                await _commentService.DeleteByIdAsync(commentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
