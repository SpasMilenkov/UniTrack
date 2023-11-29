using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTrackBackend.Data.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }

        // Navigation properties, if needed
        // public virtual User User { get; set; }
        // public virtual Event Event { get; set; }
    }
}
