using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public interface IAuthService
{
    public string GenerateJwtToken(User user);
    public Task<string> GenerateRefreshToken(User user);
    public Task<User?> GetUserFromRefreshToken(string refreshToken);
    public Task<string?> GetEmailConfirmationToken(User user);
    public Task<User> RegisterUser(RegisterDto model);
    public Task<User?> LoginUser(LoginDto model);
    public Task SignInUser(User user);
    public Task LogoutUser(User user);
    public Task<IdentityResult> ConfirmEmail(User user, string token);
    public Task<IdentityResult?> ResetPassword(ResetPasswordDto dto);
    public Task<string> GenerateForgottenPasswordLink(User user);
    public Task<User?> GetUserByEmail(string email);
    public Task<User?> GetUserById(string id);
    public Task<string> GetUserRole(User user); 
}