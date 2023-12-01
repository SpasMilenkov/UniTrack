using UniTrackBackend.Data.Models;


namespace UniTrackBackend.Data.Interfaces
{
        public interface IAbsenceService
        {
            Task<Absence?> AddAbsenceAsync(Absence? absence);
            Task<IEnumerable<Absence?>?> GetAbsencesAsync();
            Task<Absence?> GetAbsencesByStudentIdAsync(int studentId);
            Task<Absence?> UpdateAbsenceAsync(Absence? absence);
            Task<bool> DeleteAbsenceAsync(int absenceId);
            
        }
   
}
