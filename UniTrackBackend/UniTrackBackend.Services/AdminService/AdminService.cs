using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;

    public AdminService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(User user)
    {
        return await _userManager.CreateAsync(user);
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public IEnumerable<User> GetAllUsers()
    {
        // UserManager does not have a direct method to retrieve all users
        // This will depend on your UserStore implementation
        return _userManager.Users.ToList();
    }

    public async Task<IdentityResult> UpdateUserAsync(User user)
    {
        return await _userManager.UpdateAsync(user);   
    }

    public async Task<IdentityResult> DeleteUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            return await _userManager.DeleteAsync(user);
        }
        return IdentityResult.Failed(new IdentityError { Description = "User not found" });
    }
}