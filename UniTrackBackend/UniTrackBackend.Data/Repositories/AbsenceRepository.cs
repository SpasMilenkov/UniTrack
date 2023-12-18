using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Repositories;

public class AbsenceRepository:EfRepository<Absence>, IAbsenceRepository
{
    private readonly UniTrackDbContext _context;
    public AbsenceRepository(UniTrackDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Absence>> GetAllAbsencesWithDetailsAsync()
    {
        var allAbsences = await _context.Absences
            .Include(a => a.Subject)
            .Include(a => a.Teacher)
            .ThenInclude(t => t.User)
            .ToListAsync();
        return allAbsences;
    }
}