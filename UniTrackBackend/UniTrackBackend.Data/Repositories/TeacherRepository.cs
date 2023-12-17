using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Repositories;

public class TeacherRepository : EfRepository<Teacher>, ITeacherRepository
{
    private readonly UniTrackDbContext _context;
    public TeacherRepository(UniTrackDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Teacher?> GetWithDetailsAsync(int id)
    {
        var teacher = _context.Teachers
            .Include(t => t.User)
            .Include(t => t.School)
            .Include(t => t.Subjects)
            .Include(t => t.Absences)
            .Include(t => t.Marks)
            .FirstOrDefault(t => t.Id == id); // Replace yourTeacherId with the actual teacher ID

        return teacher;
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachersWithDetailsAsync()
    {
        var allTeachers = await _context.Teachers
            .Include(t => t.User)
            .Include(t => t.School)
            .Include(t => t.Subjects)
            .ToListAsync();
    
        return allTeachers;
    }
}