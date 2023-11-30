namespace UniTrackBackend.Data.Models;

public class Absence
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public DateTime Time { get; set; }
}