using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public interface IAdminService
{
    Task<IdentityResult> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(string id);
    Task<IEnumerable<User>> GetAllUsers(int schoolId);
    Task<IdentityResult> UpdateUserAsync(User user);

    Task<IdentityResult> DeleteUserAsync(string id);
    Task<Admin> GetAdminByUserId(string userId);
}