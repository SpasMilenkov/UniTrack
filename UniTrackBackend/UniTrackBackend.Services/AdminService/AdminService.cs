using UniTrackBackend.Data;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly UnitOfWork _unitOfWork;

        public AdminService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _unitOfWork.UserRepository.UpdateAsync(user);   
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _unitOfWork.UserRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }
        }

        // Implement other admin-specific methods here
    }

}
