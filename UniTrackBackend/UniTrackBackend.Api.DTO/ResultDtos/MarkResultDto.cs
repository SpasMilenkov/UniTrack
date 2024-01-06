namespace UniTrackBackend.Api.DTO.ResultDtos;

public record MarkResultDto(
    string Value,
    string StudentId,
    string TeacherId,
    string SubjectId,
    string Topic,
    string GradedOn,
    string SubjectName,
    string TeacherFirstName,
    string TeacherLastName
);