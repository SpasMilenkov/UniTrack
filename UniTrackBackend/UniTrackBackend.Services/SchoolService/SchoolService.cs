using Microsoft.Extensions.Logging;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly UnitOfWork _context;
        private readonly ILogger<SchoolService> _logger;

        public SchoolService(UnitOfWork context, ILogger<SchoolService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<School?> AddSchoolAsync(School? school)
        {
            try
            {
                await _context.SchoolRepository.AddAsync(school);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding school");
                return null;
            }
            return school;
        }
        public async Task<School?> GetSchoolByIdAsync(int id)
        {
            try
            {
                return await _context.SchoolRepository.GetByIdAsync(id);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting school by ID");
                return null;
            }


        }
        public async Task<IEnumerable<School?>?> GetAllSchoolsAsync()
        {
            try
            {
                return await _context.SchoolRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all schools");
                return null;
            }

        }
        public async Task<School?> UpdateSchoolAsync(School? school)
        {
            try
            {
                await _context.SchoolRepository.UpdateAsync(school);
                return school;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to update school");
                return null;
            }
        }

        public async Task<bool> DeleteSchoolAsync(int id)
        {
            try
            {
                var school = await _context.SchoolRepository.GetByIdAsync(id);
                if (school == null) return false;

                await _context.SchoolRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to delete school");
                return false;
            }

        }
    }
}
