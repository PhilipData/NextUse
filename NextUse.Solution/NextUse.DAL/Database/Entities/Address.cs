using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Database.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public int PostalCode { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }

        public Profile? Profile { get; set; }
        public Product? Product { get; set; }


    }
}
