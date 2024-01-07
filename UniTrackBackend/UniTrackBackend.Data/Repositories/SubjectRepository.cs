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
        IQueryable<Subject> query = _context.Subjects
            .Include(s => s.Teachers)
            .ThenInclude(t => t.User);

        if (filter.Any())
        {
            query = query.Where(s => filter.Contains(s.Id));
        }

        return await query.ToListAsync();
    }
}