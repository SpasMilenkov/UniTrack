using UniTrackBackend.Data.Models;
using UniTrackBackend.Data;
using Microsoft.Extensions.Logging;


namespace UniTrackBackend.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly UnitOfWork _context;
        private readonly ILogger<TeacherService> _logger;

        public TeacherService(UnitOfWork unitOfWork,ILogger<TeacherService> logger)
        {
            _context = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Teacher?>?> GetAllTeachersAsync()
        {
            try
            {
                return await _context.TeacherRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all teachers");
                return null;
            }

        }

      
        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            try
            {
                return await _context.TeacherRepository.GetByIdAsync(id);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting teacher by ID");
                return null;
            }


        }

       
        public async Task<Teacher?> AddTeacherAsync(Teacher? teacher)
        {
            try
            {
                await _context.TeacherRepository.AddAsync(teacher);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding teacher");
                return null;
            }
            return teacher;
        }

       
        public async Task<Teacher?> UpdateTeacherAsync(Teacher? teacher)
        {
            try
            {
                await _context.TeacherRepository.UpdateAsync(teacher);
                return teacher;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to update teacher");
                return null;
            }
        }
       
        public async Task<bool> DeleteTeacherAsync(int id)
        {
            try
            {
                var teacher = await _context.TeacherRepository.GetByIdAsync(id);
                if (teacher == null) return false;

                await _context.TeacherRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to delete teacher");
                return false;
            }

        }
    }

}
