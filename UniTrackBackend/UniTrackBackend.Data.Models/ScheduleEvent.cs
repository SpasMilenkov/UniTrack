using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTrackBackend.Data.Models
{
    public class ScheduleEvent
    {
        public int ScheduleEventId { get; set; }  // Unique identifier for the schedule event
        public DateTime StartTime { get; set; }  // Start time of the event
        public DateTime EndTime { get; set; }  // End time of the event
        public string EventName { get; set; } = null!;  // Name or description of the event
        public string EventDescription { get; set; } = null!;
        public string Location { get; set; } = null!;
    }

}
