using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.DTO.RatingDTO
{
    public class RatingResponse
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public RatingProfileResponse FromProfile { get; set; }
        public required RatingProfileResponse ToProfile { get; set; }
        public string CreatedAt { get; set; }

    }

    public class RatingProfileResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
