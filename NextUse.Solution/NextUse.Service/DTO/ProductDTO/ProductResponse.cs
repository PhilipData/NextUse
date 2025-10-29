using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.ProductDTO
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }

        public ProductCategoryResponse? Category { get; set; }
        public ProductAddressReponse? Address { get; set; }
        public ProductProfilesResponse? Profile { get; set; }
        public IEnumerable<ProductCommentResponse>? Comments { get; set; }
        public IEnumerable<ProductImageResponse>? Images { get; set; }

    }

    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProductAddressReponse
    {
        public int Id { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public int PostalCode { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
    }

    public class ProductProfilesResponse
    {
        public int Id { get; set; }
        public double AverageRating { get; set; }
        public double RatingAmount { get; set; }
        public required string Name { get; set; }
    }

    public class ProductCommentResponse
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public required string CreatedAt { get; set; }
        //public CommentProfileResponse? Profile { get; set; }
    }

    public class ProductImageResponse
    {
        public int Id { get; set; }
        public required byte[] Blob { get; set; }
    }

}
