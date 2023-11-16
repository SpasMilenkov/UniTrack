using UniTrackBackend.Models;

namespace UniTrackBackend.Interfaces;

public interface IAuthService
{
    public string GenerateJwtToken(User user);
    public Task<string> GenerateRefreshToken(User user);
    public Task<User?> GetUserFromRefreshToken(string refreshToken);
    public CookieOptions GetRefreshCookieOptions();
    public CookieOptions GetAccessCookieOptions();
}