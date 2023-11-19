using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Interfaces;

public interface IAuthService
{
    public string GenerateJwtToken(User user);
    public Task<string> GenerateRefreshToken(User user);
    public Task<User?> GetUserFromRefreshToken(string refreshToken);
    public CookieOptions GetRefreshCookieOptions();
    public CookieOptions GetAccessCookieOptions();
    public Task<bool> ApproveStudents();
    public Task<bool> ApproveParents();
    public Task<bool> ApproveTeachers();
    public Task<bool> ApproveAdmins();
}