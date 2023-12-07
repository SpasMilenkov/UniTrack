using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Repositories;

public class MarkRepository :EfRepository<Mark>
{    
    private readonly UniTrackDbContext _context;

    public MarkRepository(UniTrackDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Dictionary<string, decimal>> CalculateClassAverages(int gradeId)
    {
        return await _context.Marks
            .Include(mark => mark.Student)
            .Where(mark => mark.Student.GradeId == gradeId)
            .GroupBy(mark => mark.Subject.Name)
            .Select(group => new { Subject = group.Key, Average = group.Average(mark => mark.Value) })
            .ToDictionaryAsync(g => g.Subject, g => Math.Round(g.Average, 2));
    }
}