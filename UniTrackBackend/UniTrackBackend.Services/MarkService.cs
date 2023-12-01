using Microsoft.Extensions.Logging;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Interfaces;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services
{
    public class MarkService : IMarkService
    {
        private readonly UnitOfWork _context;
        private readonly ILogger<MarkService> _logger;

        public MarkService(UnitOfWork context, ILogger<MarkService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Mark?> AddMarkAsync(Mark? mark)
        {
            try
            {
                await _context.MarkRepository.AddAsync(mark);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding mark");
                return null;
            }
            return mark;
        }

        public async Task<Mark?> GetMarkByIdAsync(int id)
        {
            try
            {
                return await _context.MarkRepository.GetByIdAsync(id);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting mark by ID");
                return null;
            }
            
                
        }

        public async Task<IEnumerable<Mark?>?> GetAllMarksAsync()
        {
            try
            {
                return await _context.MarkRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks");
                return null;
            }
                
        }

        public async Task<IEnumerable<Mark>?> GetMarksByStudentAsync(int studentId)
        {
            try
            {
                return await _context.MarkRepository.GetAsync(m => m.Student.Id == studentId);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks a student has");
                return null;
            }
        }

        public async Task<IEnumerable<Mark?>?> GetMarksByTeacherAsync(int teacherId)
        {
            try
            {
                return await _context.MarkRepository.GetAsync(m => m.Teacher.Id == teacherId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks a teacher has graded");
                return null;
            }
                
        }

        public async Task<IEnumerable<Mark>?> GetMarksBySubjectAsync(int subjectId)
        {
            try
            {
                return await _context.MarkRepository.GetAsync(m => m.Subject.Id == subjectId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks of a current subject");
                return null;
            }
        }

        public async Task<IEnumerable<Mark?>?> GetMarksByDateAsync(DateTime date)
        {
            try
            {
                return await _context.MarkRepository.GetAsync(m => m.GradedOn.Date == date.Date);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks graded in the same day");
                return null;
            }
        }

        public async Task<Mark?> UpdateMarkAsync(Mark? mark)
        {
            try
            {
                await _context.MarkRepository.UpdateAsync(mark);
                return mark;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to update mark");
                return null;
            }
        }

        public async Task<bool> DeleteMarkAsync(int id)
        {
            try
            {
                var mark = await _context.MarkRepository.GetByIdAsync(id);
                if (mark == null) return false;
                
                await _context.MarkRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to delete mark");
                return false;
            }

        }
    }
}
