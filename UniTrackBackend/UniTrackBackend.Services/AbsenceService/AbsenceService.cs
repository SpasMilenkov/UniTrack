using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public class AbsenceService : IAbsenceService
{
    private readonly IUnitOfWork _context;

    public AbsenceService(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Absence> AddAbsenceAsync(Absence absence)
    {
           
        await _context.AbsenceRepository.AddAsync(absence);
        await _context.SaveAsync();
        return absence;
    }

    public async Task<IEnumerable<Absence>> GetAbsencesAsync()
    {
        var absences = await _context.AbsenceRepository.GetAllAbsencesWithDetailsAsync();
        var absenceList = absences.ToList();

        return absenceList;
    }

    public async Task<IEnumerable<Absence>> GetAbsencesByStudentIdAsync(int studentId)
    {
        return await _context.AbsenceRepository.GetAllAbsencesWithDetailsAsync(a => a.StudentId == studentId);
    }
    public async Task<IEnumerable<Absence>> GetAbsencesByTeacherIdAsync(int teacherId)
    {
        return await _context.AbsenceRepository.GetAllAbsencesWithDetailsAsync(a => a.TeacherId == teacherId);
    }
    public async Task UpdateAbsenceAsync(Absence updatedAbsence)
    {
        var absence = await _context.AbsenceRepository.GetByIdAsync(updatedAbsence.Id);
        if (absence == null) throw new ArgumentException("Absence not found");
        absence.Excused = updatedAbsence.Excused;
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