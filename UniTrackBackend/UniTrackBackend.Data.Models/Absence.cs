namespace UniTrackBackend.Data.Models;

public class Absence
{
    public int Id { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public DateTime Time { get; set; }
}