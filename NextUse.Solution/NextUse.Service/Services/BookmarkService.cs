namespace NextUse.Services.Services
{

    public class BookmarkService : IBookmarkService
    {

        private readonly IBookmarkRepository _bookmarkRepository;

        public BookmarkService(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        private BookmarkResponse MapBookmarkToResponse(Bookmark bookmark)
        {
            return new BookmarkResponse
            {
                Id = bookmark.Id,
                Profile = bookmark.Profile != null ? new BookmarkProfileResponse
                {
                    Id = bookmark.Profile.Id,
                    Name = bookmark.Profile.Name,
                } : null,
                Product = bookmark.Product != null ? new BookmarkProductResponse
                {
                    Id = bookmark.Product.Id,
                    Title = bookmark.Product.Title,
                    Description = bookmark.Product.Description,
                    Price = bookmark.Product.Price
                } : null
            };
        }

        private Bookmark MapRequestToBookmark(BookmarkRequest request)
        {
            return new Bookmark
            {
                ProfileId = request.ProfileId,
                ProductId = request.ProductId
            };
        }

        public async Task<IEnumerable<BookmarkResponse>> GetAllAsync()
        {
            var bookmarks = await _bookmarkRepository.GetAllAsync();
            return bookmarks.Select(MapBookmarkToResponse).ToList();
        }

        public async Task<BookmarkResponse?> GetByIdAsync(int id)
        {
            var bookmark = await _bookmarkRepository.GetByIdAsync(id);
            return bookmark is null ? null : MapBookmarkToResponse(bookmark);
        }

        public async Task<IEnumerable<BookmarkResponse>> GetByProfileIdAsync(int profileId)
        {
            var bookmarks = await _bookmarkRepository.GetByProfileIdAsync(profileId);
            return bookmarks.Select(MapBookmarkToResponse).ToList();
        }

        public async Task<BookmarkResponse> AddAsync(BookmarkRequest bookmarkRequest)
        {
            var bookmark = MapRequestToBookmark(bookmarkRequest);
            var insertedBookmark = await _bookmarkRepository.AddAsync(bookmark);
            return MapBookmarkToResponse(insertedBookmark);
        }

        public async Task<BookmarkResponse> UpdateByIdAsync(int id, BookmarkRequest updatedBookmarkRequest)
        {
            var bookmark = MapRequestToBookmark(updatedBookmarkRequest);
            var updatedBookmark = await _bookmarkRepository.UpdateByIdAsync(id, bookmark);
            return MapBookmarkToResponse(updatedBookmark);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _bookmarkRepository.DeleteByIdAsync(id);
        }


    }

}
