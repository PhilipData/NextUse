using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Database.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? ProfileId { get; set; }
        public Profile? Profile { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
