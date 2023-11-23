using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Interfaces;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly UniTrackDbContext _context;

        public AbsenceService(UniTrackDbContext context)
        {
            _context = context;
        }

        public async Task<Absence> AddAbsenceAsync(Absence absence)
        {
            _context.Absences.Add(absence);
            await _context.SaveChangesAsync();
            return absence;
        }

        public async Task<IEnumerable<Absence>> GetAbsencesAsync()
        {
            return await _context.Absences.ToListAsync();
        }

        public async Task<IEnumerable<Absence>> GetAbsencesByStudentIdAsync(int studentId)
        {
            return await _context.Absences
                                 .Where(a => a.Student.Id == studentId)
                                 .ToListAsync();
        }

        public async Task UpdateAbsenceAsync(Absence updatedAbsence)
        {
            var absence = await _context.Absences.FindAsync(updatedAbsence.Id);
            if (absence == null) throw new ArgumentException("Absence not found");

            _context.Entry(absence).CurrentValues.SetValues(updatedAbsence);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAbsenceAsync(int absenceId)
        {
            var absence = await _context.Absences.FindAsync(absenceId);
            if (absence == null) throw new ArgumentException("Absence not found");

            _context.Absences.Remove(absence);
            await _context.SaveChangesAsync();
        }
    }
}