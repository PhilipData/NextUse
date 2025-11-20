using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Database.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Address.Id")]
        public int AddressId { get; set; }
        public Address? Address { get; set; }

        [ForeignKey("Category.Id")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Profile.Id")]
        public int ProfileId { get; set; }
        public Profile? Profile { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public List<Bookmark> Bookmarks { get; set; } = new();
        public List<Image> Images { get; set; } = new();

       
    }
}
