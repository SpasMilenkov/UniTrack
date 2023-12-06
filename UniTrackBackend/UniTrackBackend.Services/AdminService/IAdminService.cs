using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.AdminService
{
    public interface IAdminService
    {
        Task<IdentityResult> CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(string id);
        IEnumerable<User> GetAllUsers();
        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> DeleteUserAsync(string id);
        //implement other admin-specific methods
    }

}
