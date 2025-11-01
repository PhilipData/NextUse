using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.DTO.CategoryDTO
{
    public class CategoryRequest
    {
        [Required]
        [StringLength(80, ErrorMessage = "Category name cannot be longer")]
        public string Name { get; set; } = string.Empty;
    }
}
