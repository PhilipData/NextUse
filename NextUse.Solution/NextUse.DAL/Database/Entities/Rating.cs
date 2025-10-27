using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Database.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int FromProfileId { get; set; }
        public Profile? FromProfile { get; set; }
        public int ToProfileId { get; set; }
        public Profile? ToProfile { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       
    }
}
