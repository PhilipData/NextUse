using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.CommentDTO
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public required string CreatedAt { get; set; }
        public CommentProfileResponse? Profile { get; set; }
        public CommentProductResponse? Product { get; set; }


    }

   public class CommentProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class CommentProfileResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
