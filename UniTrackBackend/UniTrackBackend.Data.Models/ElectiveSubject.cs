namespace UniTrackBackend.Data.Models;

public class ElectiveSubject
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Grade> Grades { get; set; } = null!;
    public List<Student> Students { get; set; } = null!;
}