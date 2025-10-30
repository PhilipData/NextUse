namespace NextUse.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public CommentResponse MapCommentToCommentResponse(Comment comment)
        {
            var copenhagenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var createdAtCopenhagen = TimeZoneInfo.ConvertTimeFromUtc(comment.CreatedAt, copenhagenTimeZone);

            CommentResponse commentResponse = new CommentResponse
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = createdAtCopenhagen.ToString("yyyy-MM-dd HH:mm:ss"),  // Formatted for readability
            };

            if (comment.Profile != null)
            {
                commentResponse.Profile = new CommentProfileResponse
                {
                    Id = comment.Profile.Id,
                    Name = comment.Profile.Name,
                };
            }

            if (comment.Product != null)
            {
                commentResponse.Product = new CommentProductResponse
                {
                    Id = comment.Product.Id,
                    Title = comment.Product.Title,
                    Description = comment.Product.Description,
                    Price = comment.Product.Price
                };
            }

            return commentResponse;
        }


        public Comment MapCommentRequestToComment(CommentRequest commentRequest)
        {
            return new Comment
            {
                Content = commentRequest.Content,
                ProfileId = commentRequest.ProfileId,
                ProductId = commentRequest.ProductId,
            };
        }

        public async Task<IEnumerable<CommentResponse>> GetAllAsync()
        {
            IEnumerable<Comment> comments = await _commentRepository.GetAllAsync();
            return comments.Select(MapCommentToCommentResponse).ToList();
        }

        public async Task<CommentResponse?> GetByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            return comment is null ? null : MapCommentToCommentResponse(comment);
        }

        public async Task<CommentResponse> AddAsync(CommentRequest newCommentRequest)
        {
            var comment = MapCommentRequestToComment(newCommentRequest);
            var insertedComment = await _commentRepository.AddAsync(comment);
            return MapCommentToCommentResponse(insertedComment);
        }

        public async Task<CommentResponse> UpdateByIdAsync(int id, CommentRequest updatedCommentRequest)
        {
            var comment = MapCommentRequestToComment(updatedCommentRequest);
            var updatedComment = await _commentRepository.UpdateByIdAsync(id, comment);
            return MapCommentToCommentResponse(updatedComment);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _commentRepository.DeleteByIdAsync(id);
        }
    }
}
