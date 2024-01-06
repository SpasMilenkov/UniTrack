using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Commons.Exceptions;

namespace UniTrackBackend.Data.Repositories;

public class SubjectRepository : EfRepository<Subject>, ISubjectRepository
{
    private readonly UniTrackDbContext _context;
    public SubjectRepository(UniTrackDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Subject> GetSubjectWithDetails(int id)
    {
        return await _context.Subjects
            .Include(s => s.Teachers)
            .FirstOrDefaultAsync(s => s.Id == id) ?? throw new DataNotFoundException();
    }

    public async Task<ICollection<Subject>> GetSubjectsWithDetails(HashSet<int> filter)
    {
        return await _context.Subjects.Include(s => s.Teachers)
            .Where(s => filter.Contains(s.Id))
            .ToListAsync();
    }
}