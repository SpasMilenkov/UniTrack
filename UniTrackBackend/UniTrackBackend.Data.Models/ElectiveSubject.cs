namespace UniTrackBackend.Data.Models;

public class ElectiveSubject
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Grade> Grades { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = null!;
}