using System.Linq.Expressions;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IGradeRepository : IRepository<Grade>
{
    Task<ICollection<Grade>> GetGradesWithDetails(Expression<Func<Grade, bool>> filter);
}