namespace NextUse.Services.Services.Interface
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponse>> GetAllAsync();
        Task<CommentResponse?> GetByIdAsync(int id);
        Task<CommentResponse> AddAsync(CommentRequest newCommentRequest);
        Task<CommentResponse> UpdateByIdAsync(int id, CommentRequest updatedCommentRequest);
        Task DeleteByIdAsync(int id);
    }
}
