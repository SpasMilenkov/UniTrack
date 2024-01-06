namespace UniTrackBackend.Api.DTO;

public record TeacherDto(
    int TeacherId,
    string FirstName,
    string LastName,
    int ClassId
    );