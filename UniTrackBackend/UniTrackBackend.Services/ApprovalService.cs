using System.Data;
using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Models.TypeSafe;

namespace UniTrackBackend.Services;

public class ApprovalService : IApprovalService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    
    public ApprovalService(UnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    public async Task<bool> ApproveStudentsAsync(List<StudentViewModel> students)
    {
        foreach (var studentModel in students)
        {
            var user = await _userManager.FindByEmailAsync(studentModel.Email);
            
            if (user is null)
                throw new DataException();
            
            await _userManager.AddToRoleAsync(user, Ts.Roles.Student);

            var grade = await _unitOfWork.GradeRepository.SingleOrDefaultAsync(g => g.Name == studentModel.Grade);
            if (grade is null)
                throw new DataException();
            
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

    public async Task<bool> ApproveParentsAsync(List<ParentViewModel> parents)
    {
        foreach (var parentModel in parents)
        {
            var user = await _userManager.FindByEmailAsync(parentModel.Email);
            
            if (user is null)
                throw new DataException();

            await _userManager.AddToRoleAsync(user, Ts.Roles.Parent);
            
        }
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> ApproveTeachersAsync(List<TeacherViewModel> teachers)
    {
        foreach (var teacherModel in teachers)
        {
            var user = await _userManager.FindByEmailAsync(teacherModel.Email);

            if (user is null)
                throw new DataException();
            
            await _userManager.AddToRoleAsync(user, Ts.Roles.Parent);
            
        }
        await _unitOfWork.SaveAsync();
        return true;
    }
    
}