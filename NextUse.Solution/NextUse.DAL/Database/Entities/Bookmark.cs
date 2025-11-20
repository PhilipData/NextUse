using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Database.Entities
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int? ProfileId { get; set; }
        public Profile? Profile { get; set; }

        public int? ProductId { get; set; } 
        public Product? Product { get; set; }
      
    }
}
