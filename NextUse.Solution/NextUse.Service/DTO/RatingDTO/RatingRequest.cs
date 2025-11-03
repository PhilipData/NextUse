using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.RatingDTO
{
    public class RatingRequest
    {
        [Required(ErrorMessage = "Score is required")]
        [Range(0, 5, ErrorMessage = "Score must be between 0 and 5")]
        public int Score { get; set; }

        [Required(ErrorMessage = "FromProfileId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "FromProfileId must not be 0")]
        public int FromProfileId { get; set; }

        [Required(ErrorMessage = "ToProfile is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ToProfile must not be 0")]
        public int ToProfileId { get; set; }
    }
}
