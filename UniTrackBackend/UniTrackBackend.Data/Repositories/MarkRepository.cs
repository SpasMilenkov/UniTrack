using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Repositories;

public class MarkRepository :EfRepository<Mark>, IMarkRepository
{    
    private readonly UniTrackDbContext _context;

    public MarkRepository(UniTrackDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<List<ClassAverage>> CalculateClassAverages(int gradeId)
    {
        var classAverages = await _context.Marks
            .Include(mark => mark.Student)
            .Where(mark => mark.Student.GradeId == gradeId)
            .GroupBy(mark => mark.Subject.Name)
            .Select(group => new ClassAverage
            {
                ClassName = group.Key,
                Average = Math.Round(group.Average(mark => mark.Value), 2)
            })
            .ToListAsync();

        return classAverages;
    }

}