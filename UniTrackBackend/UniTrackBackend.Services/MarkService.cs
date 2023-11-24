using UniTrackBackend.Data.Interfaces;
using UniTrackBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Database;

namespace UniTrackBackend.Data.Services
{
    public class MarkService : IMarkService
    {
        private readonly UnitOfWork _context;

        public MarkService(UnitOfWork context)
        {
            _context = context;
        }

        public async Task<Mark> AddMarkAsync(Mark mark)
        {
            await _context.MarkRepository.AddAsync(mark);
            return mark;
        }

        public async Task<Mark> GetMarkByIdAsync(int id)
        {
            
            return await _context.MarkRepository.GetByIdAsync(id);
                
        }

        public async Task<IEnumerable<Mark>> GetAllMarksAsync()
        {
            return await _context.MarkRepository.GetAllAsync();
                
        }

        public async Task<IEnumerable<Mark>> GetMarksByStudentAsync(int studentId)
        {
            return await _context.MarkRepository.GetAsync(m => m.Student.Id == studentId);
                
        }

        public async Task<IEnumerable<Mark>> GetMarksByTeacherAsync(int teacherId)
        {
            return await _context.MarkRepository.GetAsync(m => m.Teacher.Id == teacherId);
                
        }

        public async Task<IEnumerable<Mark>> GetMarksBySubjectAsync(int subjectId)
        {
            return await _context.MarkRepository.GetAsync(m => m.Subject.Id == subjectId);
                
        }

        public async Task<IEnumerable<Mark>> GetMarksByDateAsync(DateTime date)
        {
            return await _context.MarkRepository.GetAsync(m => m.GradedOn.Date == date.Date);

               
        }

        public async Task<Mark> UpdateMarkAsync(Mark mark)
        {
            await _context.MarkRepository.UpdateAsync(mark);
            return mark;
        }

        public async Task DeleteMarkAsync(int id)
        {
            var mark = await _context.MarkRepository.GetByIdAsync(id);
            if (mark != null)
            {
                await _context.MarkRepository.DeleteAsync(id);
                
            }
        }

       
    }
}
