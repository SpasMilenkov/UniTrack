namespace UniTrackBackend.Data.Models;

public class Student
{
    public int Id { get; set; }
    public int StudentNumber { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public int GradeId { get; set; }
    public Grade Grade { get; set; } = null!;
    public int SchoolId { get; set; }
    public School School { get; set; } = null!;
    public ICollection<Absence> Absences { get; set; } = null!;
    public ICollection<Mark> Marks { get; set; } = null!;
}