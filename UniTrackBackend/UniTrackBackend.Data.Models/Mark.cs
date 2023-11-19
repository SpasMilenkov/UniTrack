namespace UniTrackBackend.Data.Models;

public class Mark
{
    public int Id { get; set; }
    public decimal Points { get; set; }
    public Student Student { get; set; } = null!;
    public Teacher Teacher { get; set; } = null!;
    public Subject Subject { get; set; } = null!;
    public DateTime GradedOn { get; set; }  
}