using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NextUse.DAL.Database.Entities;
using NextUse.Services.Utils;

namespace NextUse.Services.DTO.ProductDTO
{
    public class ProductRequest
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Price must be between 0 and 1.000.000")]
        public required decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AddressId must not be 0")]
        public required int AddressId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ProfileId must not be 0")]
        public required int ProfileId { get; set; }
        public int? CategoryId { get; set; }

    }

    public class ProductRequestWithImages
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Price must be between 0 and 1.000.000")]
        public required decimal Price { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 chars")]
        public required string Country { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 chars")]
        public required string City { get; set; }

        [Required]
        [Range(0, 999999, ErrorMessage = "PostalCode needs to be an integer between 0 and 999999")]
        public int PostalCode { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ProfileId must not be 0")]
        public required int ProfileId { get; set; }
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "File is required.")]
        [ImageFileExtensions("jpg,jpeg,png", ErrorMessage = "Only JPG, JPEG, and PNG files are allowed.")]
        public required List<IFormFile> ImageFiles { get; set; }
    }
}
