using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.AbsenceService
{
        public interface IAbsenceService
        {
            Task<Absence> AddAbsenceAsync(Absence absence);
            Task<IEnumerable<Absence>> GetAbsencesAsync();
            Task<IEnumerable<Absence>> GetAbsencesByStudentIdAsync(int studentId);
            Task UpdateAbsenceAsync(Absence absence);
            Task DeleteAbsenceAsync(int absenceId);
            
        }
   
}
