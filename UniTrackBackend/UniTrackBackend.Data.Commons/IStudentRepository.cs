using System.Linq.Expressions;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetStudentWithDetailsAsync(int id);
    Task<Student?> GetStudentWithDetailsAsync(string id);
    Task<IEnumerable<Student>> GetStudentsWithDetailsAsync(Expression<Func<Student, bool>> filter);
    Task<IEnumerable<Student>> GetStudentsByTeacherIdAsync(int teacherId);
}
