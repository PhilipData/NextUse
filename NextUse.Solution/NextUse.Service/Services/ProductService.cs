//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NextUse.DAL.Database.Entities;
//using NextUse.DAL.Repository;
//using NextUse.DAL.Repository.Interface;
//using NextUse.Services.DTO.ProductDTO;
//using NextUse.Services.Services.Interface;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace NextUse.Services.Services
//{
//    public class ProductService : IProductService
//    {
//        private readonly IProductRepository _productsRepository;
//        private readonly IImageService _imageService;
//        //private readonly IAddressService _addressService;
//        private readonly IUnitOfWork _unitOfWork;

//        public ProductService(IProductRepository productsRepository, IImageService imageService, IUnitOfWork unitOfWork)
//        {
//            _productsRepository = productsRepository;
//            _imageService = imageService;
//            //_addressService = addressService;   
//            _unitOfWork = unitOfWork;
//        }

//        private ProductResponse MapProductsToProductsResponse(Product product)
//        {
//            var productResponse = new ProductResponse
//            {
//                Id = product.Id,
//                Title = product.Title,
//                Price = product.Price,
//                Description = product.Description,
//            };

//            if (product.Address != null)
//            {
//                productResponse.Address = new ProductAddressReponse
//                {

//                    Id = product.Address!.Id,
//                    Country = product.Address.Country,
//                    City = product.Address.City,
//                    PostalCode = product.Address.PostalCode,
//                    Street = product.Address.Street,
//                    HouseNumber = product.Address.HouseNumber
//                };
//            }

//            if (product.Profile != null)
//            {
//                productResponse.Profile = new ProductProfilesResponse
//                {
//                    Id = product.Profile!.Id,
//                    Name = product.Profile.Name,
//                    AverageRating = product.Profile.Ratings.IsNullOrEmpty() ? 0 : product.Profile.Ratings!.Where(r => r.ToProfileId == product.Profile.Id).Average(r => r.Score),
//                    RatingAmount = product.Profile.Ratings.Count
//                };
//            }

//            if (product.Category != null)
//            {
//                productResponse.Category = new ProductCategoryResponse
//                {
//                    Id = product.Category!.Id,
//                    Name = product.Category.Name
//                };
//            }
//            //if (!product.Comments.IsNullOrEmpty())
//            //{
//            //    productResponse.Comments = product.Comments?.Select(comment => new ProductCommentResponse
//            //    {
//            //        Id = comment.Id,
//            //        Content = comment.Content,
//            //        CreatedAt = comment.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
//            //        Profile = new CommentProfileResponse
//            //        {
//            //            Id = comment.Profile!.Id,
//            //            Name = comment.Profile.Name
//            //        }

//            //    });
//            //}

//            if (!product.Images.IsNullOrEmpty())
//            {
//                productResponse.Images = product.Images?.Select(image => new ProductImageResponse
//                {
//                    Id = image.Id,
//                    Blob = image.Blob,
//                });
//            }

//            return productResponse;
//        }

//        private Product MapProductRequestToProducts(ProductRequest productsRequest)
//        {
//            return new Product
//            {
//                Title = productsRequest.Title,
//                Price = productsRequest.Price,
//                Description = productsRequest.Description,
//                AddressId = productsRequest.AddressId,
//                ProfileId = productsRequest.ProfileId,
//                CategoryId = productsRequest.CategoryId
//            };
//        }

//        private Product MapProductRequestWithImagesToProducts(ProductRequestWithImages productsRequest, int addressId)
//        {
//            return new Product
//            {
//                Title = productsRequest.Title,
//                Description = productsRequest.Description,
//                Price = productsRequest.Price,
//                AddressId = addressId,
//                ProfileId = productsRequest.ProfileId,
//                CategoryId = productsRequest.CategoryId
//            };
//        }

//        //private AddressRequest MapProductRequestWithImagesAddressToAddressRequest(ProductRequestWithImages productRequest)
//        //{
//        //    return new AddressRequest
//        //    {
//        //        Country = productRequest.Country,
//        //        City = productRequest.City,
//        //        PostalCode = productRequest.PostalCode,
//        //    };
//        //}

//        private ImageRequest MapProductRequestWithImagesToImageRequest(ProductRequestWithImages productRequest, int productId)
//        {
//            return new ImageRequest
//            {
//                ProductId = productId,
//                ImageFiles = productRequest.ImageFiles,
//            };
//        }

//        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
//        {
//            IEnumerable<Product> products = await _productsRepository.GetAllAsync();
//            return products.Select(MapProductsToProductsResponse).ToList();
//        }

//        public async Task<ProductResponse?> GetByIdAsync(int id)
//        {
//            var product = await _productsRepository.GetByIdAsync(id);

//            return product is null ? null : MapProductsToProductsResponse(product);
//        }

//        public async Task<IEnumerable<ProductResponse>> GetByProfileIdAsync(int profileId)
//        {
//            IEnumerable<Product> products = await _productsRepository.GetByProfileIdAsync(profileId);
//            return products.Select(MapProductsToProductsResponse).ToList();
//        }

//        public async Task<ProductResponse> UpdateByIdAsync(int id, ProductRequest updateProductRequest)
//        {
//            var product = MapProductRequestToProducts(updateProductRequest);

//            var updatedProduct = await _productsRepository.UpdateByIdAsync(id, product);

//            return MapProductsToProductsResponse(updatedProduct);
//        }

//        public async Task DeleteByIdAsync(int id)
//        {
//            await _productsRepository.DeleteByIdAsync(id);
//        }

//        public async Task<ProductResponse> AddAsync(ProductRequest newProduct)
//        {
//            var product = MapProductRequestToProducts(newProduct);

//            var insertedProduct = await _productsRepository.AddAsync(product);

//            return MapProductsToProductsResponse(insertedProduct);

//        }

//        //public async Task<ProductResponse> AddWithImagesAsync(ProductRequestWithImages newProduct)
//        //{
//        //    await _unitOfWork.BeginTransactionAsync();

//        //    try
//        //    {
//        //        // Add Address
//        //        var addressRequest = MapProductRequestWithImagesAddressToAddressRequest(newProduct);

//        //        var insertedAddress = await _addressService.AddAsync(addressRequest);

//        //        if (insertedAddress is null)
//        //        {
//        //            throw new Exception("Address wasn't added");
//        //        }

//        //        // Add product
//        //        var product = MapProductRequestWithImagesToProducts(newProduct, insertedAddress.Id);

//        //        var insertedProduct = await _productsRepository.AddAsync(product);

//        //        if (insertedProduct is null)
//        //        {
//        //            throw new Exception("Product wasn't added");
//        //        }

//        //        // Add images
//        //        var imageRequest = MapProductRequestWithImagesToImageRequest(newProduct, insertedProduct.Id);

//        //        await _imageService.AddRangeAsync(imageRequest);

//        //        var updatedProduct = await _productsRepository.GetByIdAsync(insertedProduct.Id);

//        //        if (updatedProduct is null)
//        //        {
//        //            throw new Exception("New product wasn't found");
//        //        }

//        //        await _unitOfWork.CommitTransactionAsync();

//        //        return MapProductsToProductsResponse(updatedProduct);
//        //    }
//        //    catch
//        //    {
//        //        // Rollback transaction if there’s any error
//        //        await _unitOfWork.RollbackTransactionAsync();
//        //        throw;
//        //    }

//        //}

//        public async Task<ProductResponse?> GetByTitleAsync(string title)
//        {
//            var product = await _productsRepository.GetByTitleAsync(title);
//            return product is null ? null : MapProductsToProductsResponse(product);
//        }

//        public Task<ProductResponse> AddWithImagesAsync(ProductRequestWithImages newProduct)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
