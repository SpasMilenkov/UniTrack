using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Commons.Exceptions;

namespace UniTrackBackend.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
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

    public async Task<Admin> GetAdminByUserId(string userId)
    {
        var admin = await _unitOfWork.AdminRepository.FirstOrDefaultAsync(a => a.UserId == userId);
        if (admin is null) throw new DataNotFoundException("No admin with matching credentials has been found");
        await _unitOfWork.AdminRepository.LoadReferenceAsync(admin, a => a.User);
        await _unitOfWork.AdminRepository.LoadReferenceAsync(admin, a => a.School);
        return admin;
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