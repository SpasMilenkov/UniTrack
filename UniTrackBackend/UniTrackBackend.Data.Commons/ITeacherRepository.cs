using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface ITeacherRepository: IRepository<Teacher>
{
    Task<Teacher?> GetWithDetailsAsync(int id);
    public Task<IEnumerable<Teacher>> GetAllTeachersWithDetailsAsync();
}