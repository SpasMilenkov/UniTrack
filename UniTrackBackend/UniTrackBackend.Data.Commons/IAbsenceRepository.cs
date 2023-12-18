using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IAbsenceRepository : IRepository<Absence>
{
    public Task<IEnumerable<Absence>> GetAllAbsencesWithDetailsAsync();
}