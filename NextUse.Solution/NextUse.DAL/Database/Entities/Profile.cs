using Microsoft.EntityFrameworkCore.Metadata;
using NextUse.DAL.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Database.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public string UserId { get; set; }
        //public virtual IKey UserId { get; set; }
        public User? User { get; set; }
        public int AddressId { get; set; }

        public Address? Address { get; set; }
        public List<Rating> Ratings { get; set; } = new();
        public List<Bookmark> Bookmarks { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public bool IsBlocked { get; set; } = false;
    }
}
