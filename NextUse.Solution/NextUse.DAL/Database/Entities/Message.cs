using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Database.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? FromProfileId { get; set; }
        public Profile? FromProfile { get; set; }
        public int? ToProfileId { get; set; }
        public Profile? ToProfile { get; set; }
    }
}
