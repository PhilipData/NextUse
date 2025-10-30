using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.DTO.MessageDTO
{
    public class MessageResponse
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public required string CreatedAt { get; set; }
        public MessageProfileResponse? FromProfile { get; set; }
        public MessageProfileResponse? ToProfile { get; set; }
    }

    public class MessageProfileResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
