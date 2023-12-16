using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IUnitOfWork : IDisposable
{
    IRepository<School> SchoolRepository { get; }
    IRepository<Absence> AbsenceRepository { get; }
    IRepository<Grade> GradeRepository { get; }
    IMarkRepository MarkRepository { get; }
    IRepository<Parent> ParentRepository { get; }
    IStudentRepository StudentRepository { get; }
    IRepository<Subject> SubjectRepository { get; }
    IRepository<Teacher> TeacherRepository { get; }
    IRepository<Admin> AdminRepository { get; }
    Task SaveAsync();
}