namespace UniTrackBackend.Data.Models;

public class Grade
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Subject> Subjects { get; set; } = null!;
    public int SchoolId { get; set; }
    public School School { get; set; }
    public int ClassTeacherId { get; set; }
    public Teacher ClassTeacher { get; set; } = null!;
    public ICollection<Teacher> Teachers { get; set; } = null!;
    public ICollection<GradeSubjectTeacher> GradeSubjectTeachers { get; set; }
}