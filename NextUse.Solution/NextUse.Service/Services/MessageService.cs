using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public MessageResponse MapMessageToMessageResponse(Message message)
        {
            var copenhagenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var createdAtCopenhagen = TimeZoneInfo.ConvertTimeFromUtc(message.CreatedAt, copenhagenTimeZone);

            MessageResponse messageResponse = new MessageResponse
            {
                Id = message.Id,
                Content = message.Content,
                CreatedAt = createdAtCopenhagen.ToString("yyyy-MM-dd HH:mm:ss"),  // Formatted for readability
            };

            if (message.ToProfile != null)
            {
                messageResponse.ToProfile = new MessageProfileResponse
                {
                    Id = message.ToProfile.Id,
                    Name = message.ToProfile.Name,
                };
            }

            if (message.FromProfile != null)
            {
                messageResponse.FromProfile = new MessageProfileResponse
                {
                    Id = message.FromProfile.Id,
                    Name = message.FromProfile.Name,
                };
            }

            return messageResponse;
        }


        public Message MapMessageRequestToMessage(MessageRequest messageRequest)
        {
            return new Message
            {
                Content = messageRequest.Content,
                FromProfileId = messageRequest.FromProfileId,
                ToProfileId = messageRequest.ToProfileId,
            };
        }

        public async Task<IEnumerable<MessageResponse>> GetAllAsync()
        {
            IEnumerable<Message> messages = await _messageRepository.GetAllAsync();
            return messages.Select(MapMessageToMessageResponse).ToList();
        }

        public async Task<MessageResponse?> GetByIdAsync(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            return message is null ? null : MapMessageToMessageResponse(message);
        }

        public async Task<MessageResponse> AddAsync(MessageRequest newMessageRequest)
        {
            var message = MapMessageRequestToMessage(newMessageRequest);
            var insertedMessage = await _messageRepository.AddAsync(message);
            return MapMessageToMessageResponse(insertedMessage);
        }

        public async Task<MessageResponse> UpdateByIdAsync(int id, MessageRequest updatedMessageRequest)
        {
            var message = MapMessageRequestToMessage(updatedMessageRequest);
            var updatedMessage = await _messageRepository.UpdateByIdAsync(id, message);
            return MapMessageToMessageResponse(updatedMessage);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _messageRepository.DeleteByIdAsync(id);
        }
    }
}
