using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTrackBackend.Api.ViewModels
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
    }

}
