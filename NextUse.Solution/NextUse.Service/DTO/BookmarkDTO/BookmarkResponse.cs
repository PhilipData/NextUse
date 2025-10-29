using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.BookmarkDTO
{
    public class BookmarkResponse
    {
        public int Id { get; set; }
        public BookmarkProductResponse?  Product { get; set; }
        public BookmarkProfileResponse? Profile { get; set; }


    }

    public class BookmarkProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class BookmarkProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
