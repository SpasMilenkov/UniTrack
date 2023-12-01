namespace UniTrackBackend.Data.Models;

public class Grade
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Subject> Subjects { get; set; } = null!;
    public int ClassTeacherId { get; set; }
    public Teacher ClassTeacher = null!;
}