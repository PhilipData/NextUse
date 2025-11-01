namespace NextUse.Services.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        private ImageResponse MapImageToImageResponse(Image image)
        {
            return new ImageResponse
            {
                Id = image.Id,
                Blob = image.Blob,
            };
        }

        private IEnumerable<Image> MapImageRequestToImage(ImageRequest imageRequest)
        {
            var images = new List<Image>();

            foreach (var file in imageRequest.ImageFiles)
            {
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);

                        byte[] imageData = memoryStream.ToArray();

                        var image = new Image
                        {
                            Blob = imageData,
                            ProductId = imageRequest.ProductId,
                        };

                        images.Add(image);
                    }
                }
            }

            return images;
        }

        public async Task<IEnumerable<ImageResponse>> GetAllAsync()
        {
            IEnumerable<Image> images = await _imageRepository.GetAllAsync();

            return images.Select(MapImageToImageResponse).ToList();
        }

        public async Task<ImageResponse?> GetByIdAsync(int id)
        {
            var image = await _imageRepository.GetByIdAsync(id);

            return image is null ? null : MapImageToImageResponse(image);
        }

        public async Task<IEnumerable<ImageResponse>?> GetByProductIdAsync(int productId)
        {
            var images = await _imageRepository.GetByProductIdAsync(productId);

            return images.IsNullOrEmpty() ? null : images!.Select(MapImageToImageResponse);
        }

        public async Task<IEnumerable<ImageResponse>> AddRangeAsync(ImageRequest newImageRequest)
        {
            var image = MapImageRequestToImage(newImageRequest);

            var insertedImages = await _imageRepository.AddRangeAsync(image);

            return insertedImages.Select(MapImageToImageResponse);
        }

        public async Task<ImageResponse> UpdateByIdAsync(int id, ImageRequest updatedImageRequest)
        {
            var image = MapImageRequestToImage(updatedImageRequest).Single();

            var updatedImage = await _imageRepository.UpdateByIdAsync(id, image);

            return MapImageToImageResponse(updatedImage);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _imageRepository.DeleteByIdAsync(id);
        }
    }
}
