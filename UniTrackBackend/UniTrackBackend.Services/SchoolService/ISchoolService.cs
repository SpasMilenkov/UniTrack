using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public interface ISchoolService
{
    Task<School?> AddSchoolAsync(string schoolName);
    Task<School?> GetSchoolByIdAsync(int id);
    Task<IEnumerable<School>?> GetAllSchoolsAsync();
    Task<School> UpdateSchoolAsync(School school);
    Task<bool> DeleteSchoolAsync(int id);
}