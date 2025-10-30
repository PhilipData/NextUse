namespace NextUse.Services.Services.Interface
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageResponse>> GetAllAsync();
        Task<MessageResponse?> GetByIdAsync(int id);
        Task<MessageResponse> AddAsync(MessageRequest newMessageRequest);
        Task<MessageResponse> UpdateByIdAsync(int id, MessageRequest updatedMessageRequest);
        Task DeleteByIdAsync(int id);
    }
}
