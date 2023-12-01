using UniTrackBackend.Data;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.AbsenceService
{
    public class AbsenceService : IAbsenceService
    {
        private readonly UnitOfWork _context;

        public AbsenceService(UnitOfWork context)
        {
            _context = context;
        }

        public async Task<Absence> AddAbsenceAsync(Absence absence)
        {
           
            await _context.AbsenceRepository.AddAsync(absence);
            return absence;
        }

        public async Task<IEnumerable<Absence>> GetAbsencesAsync()
        {
            return await _context.AbsenceRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Absence>> GetAbsencesByStudentIdAsync(int studentId)
        {
            return await _context.AbsenceRepository.GetAsync(a => a.Student.Id == studentId);
                                 
                                 
        }

        public async Task UpdateAbsenceAsync(Absence updatedAbsence)
        {
            var absence = await _context.AbsenceRepository.GetByIdAsync(updatedAbsence.Id);
            if (absence == null) throw new ArgumentException("Absence not found");

            await _context.AbsenceRepository.UpdateAsync(absence);
            await _context.SaveAsync();
        }

        public async Task DeleteAbsenceAsync(int absenceId)
        {
            var absence = await _context.AbsenceRepository.GetByIdAsync(absenceId);
            if (absence == null) throw new ArgumentException("Absence not found");

            await _context.AbsenceRepository.DeleteAsync(absenceId);
            await _context.SaveAsync();
        }
    }
}