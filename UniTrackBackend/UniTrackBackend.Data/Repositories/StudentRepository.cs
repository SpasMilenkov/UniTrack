using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Repositories;

public class StudentRepository: EfRepository<Student>, IStudentRepository
{
    private readonly UniTrackDbContext _context;

    public StudentRepository(UniTrackDbContext context) : base(context)
    {
        _context = context;
    }
    // Method to get a student with all related data
    public async Task<Student?> GetStudentWithDetailsAsync(int id)
    {
        // Eagerly load directly related entities (e.g., User and Grade)
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Grade)
                .ThenInclude(g => g.ClassTeacher)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(s => s.Id == id);
            

        if (student == null) return student;
        {
            // Load Absences - Consider filtering or limiting the results
            // For example, loading only the recent absences
            await _context.Entry(student).Collection(s => s.Absences)
                .Query()
                .Include(a => a.Subject) // Eagerly load Subject
                .LoadAsync();

            // Load Marks - Similar considerations as Absences
            await _context.Entry(student).Collection(s => s.Marks)
                .Query() // Possibly add filters or sorting
                .LoadAsync();
        }
        return student;
    }
    public async Task<Student?> GetStudentWithDetailsAsync(string id)
    {
        // Eagerly load directly related entities (e.g., User and Grade)
        var student = await _context.Students
            .Include(s => s.User)
            .Include(s => s.Grade)
            .ThenInclude(g => g.ClassTeacher)
            .ThenInclude(t => t.User)
            .FirstOrDefaultAsync(s => s.UserId == id);
            

        if (student == null) return student;
        {
            // Load Absences - Consider filtering or limiting the results
            // For example, loading only the recent absences
            await _context.Entry(student).Collection(s => s.Absences)
                .Query()
                .Include(a => a.Subject) // Eagerly load Subject
                .LoadAsync();

            // Load Marks - Similar considerations as Absences
            await _context.Entry(student).Collection(s => s.Marks)
                .Query() // Possibly add filters or sorting
                .LoadAsync();
        }
        return student;
    }
}