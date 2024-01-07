namespace UniTrackBackend.Data.Models;

public class GradeSubjectTeacher
{
    public int GradeId { get; set; }
    public Grade Grade { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

}