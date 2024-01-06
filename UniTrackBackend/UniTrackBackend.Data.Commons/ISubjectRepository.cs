using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface ISubjectRepository : IRepository<Subject>
{
    Task<Subject> GetSubjectWithDetails(int id);
    Task<ICollection<Subject>> GetSubjectsWithDetails(HashSet<int> filter);
}