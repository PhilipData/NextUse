using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.CategoryDTO
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<CategoryProductResponse> Products { get; set; } = new();
    }

    public class CategoryProductResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
    }
}
