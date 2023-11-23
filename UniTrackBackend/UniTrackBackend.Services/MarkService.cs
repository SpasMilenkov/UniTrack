using UniTrackBackend.Data.Interfaces;
using UniTrackBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Database;

namespace UniTrackBackend.Data.Services
{
    public class MarkService : IMarkService
    {
        private readonly UniTrackDbContext _context;

        public MarkService(UniTrackDbContext context)
        {
            _context = context;
        }

        public async Task<Mark> AddMarkAsync(Mark mark)
        {
            _context.Marks.Add(mark);
            await _context.SaveChangesAsync();
            return mark;
        }

        public async Task<Mark> GetMarkByIdAsync(int id)
        {
            return await _context.Marks
                .Include(m => m.Student)
                .Include(m => m.Teacher)
                .Include(m => m.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Mark>> GetAllMarksAsync()
        {
            return await _context.Marks
                .Include(m => m.Student)
                .Include(m => m.Teacher)
                .Include(m => m.Subject)
                .ToListAsync();
        }

        public async Task<IEnumerable<Mark>> GetMarksByStudentAsync(int studentId)
        {
            return await _context.Marks
                .Where(m => m.Student.Id == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Mark>> GetMarksByTeacherAsync(int teacherId)
        {
            return await _context.Marks
                .Where(m => m.Teacher.Id == teacherId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Mark>> GetMarksBySubjectAsync(int subjectId)
        {
            return await _context.Marks
                .Where(m => m.Subject.Id == subjectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Mark>> GetMarksByDateAsync(DateTime date)
        {
            return await _context.Marks
                .Where(m => m.GradedOn.Date == date.Date)
                .ToListAsync();
        }

        public async Task<Mark> UpdateMarkAsync(Mark mark)
        {
            _context.Marks.Update(mark);
            await _context.SaveChangesAsync();
            return mark;
        }

        public async Task DeleteMarkAsync(int id)
        {
            var mark = await _context.Marks.FindAsync(id);
            if (mark != null)
            {
                _context.Marks.Remove(mark);
                await _context.SaveChangesAsync();
            }
        }

       
    }
}
