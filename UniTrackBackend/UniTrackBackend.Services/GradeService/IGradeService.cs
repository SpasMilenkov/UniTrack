using UniTrackBackend.Api.DTO.ResultDtos;

namespace UniTrackBackend.Services;

public interface IGradeService
{
    //Checks if a teacher is a class teacher or not 
    //Returns a key value pair with the the id of the class
    //and the name of the class as strings
    Task<KeyValuePair<string, string>?> GetClassTeacherData(int teacherId);

    Task<IEnumerable<GradeResultDto>> GetAllGradesBySchoolId(int schoolId);
}