using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetStudentWithDetailsAsync(int id);
}
