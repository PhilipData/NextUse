using Microsoft.AspNetCore.Http;
using NextUse.Services.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.ImageDTO
{
    public class ImageRequest
    {
        [Required(ErrorMessage = "File is required.")]
        [ImageFileExtensions("jpg,jpeg,png", ErrorMessage = "Only JPG, JPEG, and PNG files are allowed.")]
        public required List<IFormFile> ImageFiles { get; set; }

        [Required(ErrorMessage = "Product is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must not be 0")]
        public int ProductId { get; set; }
    }
}
