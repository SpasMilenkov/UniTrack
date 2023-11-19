namespace UniTrackBackend.Data.Models;

public class Student
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;

    public User User { get; set; } = null!;
    public Grade Grade { get; set; } = null!;
    public List<Absence> Absences { get; set; } = null!;
    public List<Mark> Marks { get; set; } = null!;
}