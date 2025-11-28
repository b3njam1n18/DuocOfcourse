using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Notifications
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Kind { get; set; } = string.Empty;
        public string? Payload { get; set; }
        public string Channel { get; set; } = "EMAIL";
        public string Status { get; set; } = "PENDING";
        public DateTime? ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public string? ErrorMsg { get; set; }
        public DateTime CreatedAt { get; set; }

        public Users User { get; set; }
    }
}
