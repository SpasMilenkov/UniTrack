using Microsoft.Extensions.Logging;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Commons.Exceptions;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IUnitOfWork _context;
        private readonly ILogger<SchoolService> _logger;
        private readonly IMapper _mapper;
        public SchoolService(IUnitOfWork context, ILogger<SchoolService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<School?> AddSchoolAsync(string schoolName)
        {
            try
            {
                var school = new School()
                {
                    Name = schoolName
                };
                await _context.SchoolRepository.AddAsync(school);
                await _context.SaveAsync();
                return school;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding school");
                return null;
            }
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
        public async Task<IEnumerable<School>?> GetAllSchoolsAsync()
        {
            try
            {
                return await _context.SchoolRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all schools");
                throw new DataNotFoundException("No registered schools");
            }

        }
        public async Task<School> UpdateSchoolAsync(School school)
        {
            try
            {
                await _context.SchoolRepository.UpdateAsync(school);
                return school;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to update school");
                throw new ArgumentException("Invalid input, update failed");
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
