namespace NextUse.Services.Services.Interface
{
    public interface IImageService
    {
        Task<IEnumerable<ImageResponse>> GetAllAsync();
        Task<ImageResponse?> GetByIdAsync(int id);
        Task<IEnumerable<ImageResponse>?> GetByProductIdAsync(int productId);
        Task<IEnumerable<ImageResponse>> AddRangeAsync(ImageRequest newImageRequest);
        Task<ImageResponse> UpdateByIdAsync(int id, ImageRequest updatedImageRequest);
        Task DeleteByIdAsync(int id);
    }
}
