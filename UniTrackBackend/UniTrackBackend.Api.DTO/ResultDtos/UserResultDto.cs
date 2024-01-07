namespace UniTrackBackend.Api.DTO.ResultDtos;

public record UserResultDto(
    string UserId, 
    string FirstName,
    string LastName,
    string Email,
    string AvatarUrl
);