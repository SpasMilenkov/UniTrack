namespace UniTrackBackend.Api.DTO.ResultDtos;

public record GradeResultDto(
    string ClassId,
    string ClassName,
    IEnumerable<StudentResultDto> Students,
    TeacherResultDto ClassTeacher
    );