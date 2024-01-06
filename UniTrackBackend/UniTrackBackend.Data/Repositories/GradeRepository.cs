using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Repositories;

public class GradeRepository : EfRepository<Grade>, IGradeRepository
{
    private readonly UniTrackDbContext _context;
    private readonly DbSet<Grade> _dbSet;
    public GradeRepository(UniTrackDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Grade>();
    }

    public async Task<ICollection<Grade>> GetGradesWithDetails(Expression<Func<Grade, bool>> filter)
    {
        return await _dbSet
            .Include(g => g.ClassTeacher)
            .ThenInclude(t => t.User)
            .Include(g => g.ClassTeacher)
            .ThenInclude(t => t.Subjects)
            .Where(filter).ToListAsync();
    }
    
}