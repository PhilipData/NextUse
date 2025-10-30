using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.ImageDTO
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public required byte[] Blob { get; set; }
        //public ImageProductResponse? Product { get; set; }
    }
}
