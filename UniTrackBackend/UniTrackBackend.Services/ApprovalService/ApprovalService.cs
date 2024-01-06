using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Models.TypeSafe;

namespace UniTrackBackend.Services;

public class ApprovalService : IApprovalService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ApprovalService> _logger;

    public ApprovalService(IUnitOfWork unitOfWork, UserManager<User> userManager, ILogger<ApprovalService> logger)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<bool> ApproveStudentsAsync(StudentApprovalDto student)
    {
        try
        {
            var filter = student.StudentIds.ToHashSet();

            var existingStudents = await _unitOfWork.StudentRepository.GetAsync(
                s => filter.Contains(s.UserId));

            for (var i = 0; i < student.StudentIds.Count; i++)
            {
                var userId = student.StudentIds[i];
                var user = await _userManager.FindByIdAsync(userId);

                if (user is null) continue;

                if (!existingStudents.All(s => s.UserId != userId)) continue;

                var newEntry = new Student()
                {
                    UserId = userId,
                    SchoolId = student.SchoolId,
                    GradeId = student.GradeId,
                    StudentNumber = i
                };
                await _unitOfWork.StudentRepository.AddAsync(newEntry);
                await _userManager.AddToRoleAsync(user, Ts.Roles.Student);
            }

            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> ApproveParentsAsync(ParentDto approvalModel)
    {
    
        var user = await _userManager.FindByIdAsync(approvalModel.UserId);
        if (user is null) throw new ArgumentException(nameof(user));
        var filter = approvalModel.ChildIds.ToHashSet();
        var children = await _unitOfWork.StudentRepository.GetAsync(s => filter.Contains(s.Id));

        var enumerable = children.ToList();
        if (children is null) throw new ArgumentException(nameof(children));
        
        var entity = new Parent()
        {
            UserId = approvalModel.UserId,
            Children = enumerable
        };
        await _unitOfWork.ParentRepository.AddAsync(entity);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> ApproveTeacherAsync(TeacherApprovalDto approvalModel)
    {
        var teacherUId = approvalModel.UserId;

        // Validate the teacher ID
        var teacherExists = await _unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == teacherUId);
        if (teacherExists != null) return false;
        
        var filter = approvalModel.SubjectIds.ToHashSet();
        var subjects = await _unitOfWork.SubjectRepository.GetAsync(s => filter.Contains(s.Id));
        var teacher = new Teacher()
        {
            // Assign the teacher to the class
            UserId = teacherUId,
            SchoolId = approvalModel.SchoolId,
            Subjects = subjects.ToList(),
        };
        await _unitOfWork.TeacherRepository.AddAsync(teacher);
        await _unitOfWork.SaveAsync();
        if (approvalModel.ClassId is -1) return true;


        var grade = await _unitOfWork.GradeRepository.GetByIdAsync(approvalModel.ClassId);
        var newTeacher = await _unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == teacherUId);
        grade.ClassTeacherId = newTeacher.Id;
        await _unitOfWork.GradeRepository.UpdateAsync(grade);
        await _unitOfWork.SaveAsync();
        return true;
    }
}