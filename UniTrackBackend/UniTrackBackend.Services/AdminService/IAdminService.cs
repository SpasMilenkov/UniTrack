using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.AdminService
{
    public interface IAdminService
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        //implement other admin-specific methods
    }

}
