namespace NextUse.Services.Services.Interface
{
    public interface IBookmarkService
    {
        Task<IEnumerable<BookmarkResponse>> GetAllAsync();
        Task<IEnumerable<BookmarkResponse>> GetByProfileIdAsync(int profileId);
        Task<BookmarkResponse?> GetByIdAsync(int id);
        Task<BookmarkResponse> AddAsync(BookmarkRequest bookmark);
        Task<BookmarkResponse> UpdateByIdAsync(int id, BookmarkRequest updatedBookmark);
        Task DeleteByIdAsync(int id);
    }
}
