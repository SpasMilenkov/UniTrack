using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Data.Interfaces;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly UnitOfWork _context;
        private readonly ILogger<AbsenceService> _logger;

        public AbsenceService(UnitOfWork context, ILogger<AbsenceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Absence?> AddAbsenceAsync(Absence? absence)
        {
            try
            {
                await _context.AbsenceRepository.AddAsync(absence);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding absence");
                return null;
            }
            return absence;
        }

        public async Task<IEnumerable<Absence?>?> GetAbsencesAsync()
        {
            try
            {
                return await _context.AbsenceRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all absences");
                return null;
            }

        }


        public async Task<Absence?> GetAbsencesByStudentIdAsync(int id)
        {
            try
            {
                return await _context.AbsenceRepository.GetByIdAsync(id);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting absence by ID");
                return null;
            }


        }

        public async Task<Absence?> UpdateAbsenceAsync(Absence? absence)
        {
            try
            {
                await _context.AbsenceRepository.UpdateAsync(absence);
                return absence;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to update absence/s");
                return null;
            }
        }

        public async Task<bool> DeleteAbsenceAsync(int id)
        {
            try
            {
                var absence = await _context.AbsenceRepository.GetByIdAsync(id);
                if (absence == null) return false;

                await _context.AbsenceRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to delete absence");
                return false;
            }

        }
    }
}