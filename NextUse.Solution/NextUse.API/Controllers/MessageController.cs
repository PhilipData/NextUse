using Microsoft.AspNetCore.Mvc;
using NextUse.Services.DTO.MessageDTO;
using NextUse.Services.Services.Interface;

namespace NextUse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<MessageResponse> messages = await _messageService.GetAllAsync();

                if (!messages.Any())
                {
                    return NoContent();
                }

                return Ok(messages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MessageRequest newMessage)
        {
            try
            {
                return Ok(await _messageService.AddAsync(newMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> FindById([FromRoute] int messageId)
        {
            try
            {
                var messageResponse = await _messageService.GetByIdAsync(messageId);

                if (messageResponse == null)
                {
                    return NotFound();
                }

                return Ok(messageResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{messageId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int messageId, [FromBody] MessageRequest updateMessage)
        {
            try
            {
                var messageResponse = await _messageService.UpdateByIdAsync(messageId, updateMessage);

                if (messageResponse == null)
                {
                    return NotFound();
                }

                return Ok(messageResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int messageId)
        {
            try
            {
                await _messageService.DeleteByIdAsync(messageId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
