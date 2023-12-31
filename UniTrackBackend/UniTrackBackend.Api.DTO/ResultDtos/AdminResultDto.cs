using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Api.DTO.ResultDtos;

public record AdminResultDto(
    string FirstName,
    string LastName,
    string Email,
    string AvatarUrl,
    string SchoolName,
    string SchoolId
    );