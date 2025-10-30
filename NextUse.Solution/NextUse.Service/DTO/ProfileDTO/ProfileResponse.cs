using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.DTO.ProfileDTO
{
    public class ProfileResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double AverageRating { get; set; }
        public int RatingAmount { get; set; }
        public required ProfileAddressResponse Address { get; set; }
        public IEnumerable<ProfileBookmarkResponse?> Bookmarks { get; set; }
        public IEnumerable<ProfileProductResponse> Products { get; set; } = [];
    }

    public class ProfileAddressResponse
    {
        public int Id { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public int PostalCode { get; set; }
        public string? Street { get; set; }
        public string? Housenumber { get; set; }


    }

    public class ProfileBookmarkResponse
    {
        public int Id { get; set; }
        public BookmarkProduckResponse? Product { get; set; }

    }

    public class ProfileProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
    }
}
