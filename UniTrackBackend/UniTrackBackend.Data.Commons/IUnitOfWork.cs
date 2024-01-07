using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IUnitOfWork : IDisposable
{
    IRepository<School> SchoolRepository { get; }
    IRepository<GradeSubjectTeacher> GradeSubjectTeacherRepository { get; }
    IAbsenceRepository AbsenceRepository { get; }
    IGradeRepository GradeRepository { get; }
    IMarkRepository MarkRepository { get; }
    IRepository<Parent> ParentRepository { get; }
    IStudentRepository StudentRepository { get; }
    ISubjectRepository SubjectRepository { get; }
    ITeacherRepository TeacherRepository { get; }
    IRepository<Admin> AdminRepository { get; }
    Task SaveAsync();
}