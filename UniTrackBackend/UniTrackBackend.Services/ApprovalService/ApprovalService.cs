using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Models.TypeSafe;

namespace UniTrackBackend.Services.ApprovalService;

public class ApprovalService : IApprovalService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ApprovalService> _logger;

    public ApprovalService(UnitOfWork unitOfWork, UserManager<User> userManager, ILogger<ApprovalService> logger)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
    }
    public async Task<bool> ApproveStudentsAsync(List<StudentViewModel> students)
    {
        try
        {
            foreach (var studentModel in students)
            {
                var user = await _userManager.FindByEmailAsync(studentModel.Email);

                if (user is null)
                    return false;
            
                await _userManager.AddToRoleAsync(user, Ts.Roles.Student);

                var grade = await _unitOfWork.GradeRepository.SingleOrDefaultAsync(g => g.Name == studentModel.Grade);
                if (grade is null)
                    return false;
            
                var student = new Student
                {
                
                    Grade = grade,
                    UserId = user.Id,
                    User = user
                };

                await _unitOfWork.StudentRepository.AddAsync(student);
            }
            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the approval");
            throw;
        }
    }

    public async Task<bool> ApproveParentsAsync(List<ParentViewModel> parents)
    {
        try
        {
            foreach (var parentModel in parents)
            {
                var user = await _userManager.FindByEmailAsync(parentModel.Email);

                if (user is null)
                    return false;

                await _userManager.AddToRoleAsync(user, Ts.Roles.Parent);
            
            }
            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the approval");
            throw;
        }

    }

    public async Task<bool> ApproveTeachersAsync(List<TeacherViewModel> teachers)
    {
        try
        {
            foreach (var teacherModel in teachers)
            {
                var user = await _userManager.FindByEmailAsync(teacherModel.Email);

                if (user is null)
                    return false;
            
                await _userManager.AddToRoleAsync(user, Ts.Roles.Parent);
            
            }
            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the approval");
            throw;
        }
    }
    
}