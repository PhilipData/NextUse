using Microsoft.AspNetCore.Identity;
using NextUse.DAL.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextUse.DAL.Extensions
{
    public class User : IdentityUser
    {
        public Profile? Profile { get; set; }
    }
}
