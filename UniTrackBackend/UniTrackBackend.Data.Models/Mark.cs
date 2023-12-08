namespace UniTrackBackend.Data.Models;

public class Mark
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
    public DateTime GradedOn { get; set; }
    public string Topic { get; set; } = null!;
}