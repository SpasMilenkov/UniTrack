using System.Linq.Expressions;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IAbsenceRepository : IRepository<Absence>
{
    public Task<IEnumerable<Absence>> GetAllAbsencesWithDetailsAsync();
    Task<IEnumerable<Absence>> GetAllAbsencesWithDetailsAsync(Expression<Func<Absence, bool>> filter);
}