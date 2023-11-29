namespace UniTrackBackend.Api.ViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; } 
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;    
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
       
    }
}
