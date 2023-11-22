namespace UniTrackBackend.Data.Models;

public class Teacher
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public List<Subject> Subjects { get; set; } = null!;
    public List<Absence> Absences { get; set; } = null!;
    public List<Mark> Marks { get; set; } = null!;
}