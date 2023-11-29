namespace UniTrackBackend.Data.Models
{ 
    public class UserSchedule
    {
        public int UserId { get; set; }  // Unique identifier for the user
        public List<ScheduleEvent> Events { get; set; }  // List of events in the user's schedule

        public UserSchedule()
        {
            Events = new List<ScheduleEvent>();
        }
    }
}
