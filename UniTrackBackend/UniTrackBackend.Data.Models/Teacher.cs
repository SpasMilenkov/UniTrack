namespace UniTrackBackend.Data.Models;

public class Teacher
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public ICollection<Subject> Subjects { get; set; } = null!;
    public ICollection<Absence> Absences { get; set; } = null!;
    public ICollection<Mark> Marks { get; set; } = null!;
}