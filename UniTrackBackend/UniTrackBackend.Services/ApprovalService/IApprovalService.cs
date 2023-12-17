using UniTrackBackend.Api.ViewModels;

namespace UniTrackBackend.Services;

public interface IApprovalService
{
    public Task<bool> ApproveStudentsAsync(StudentApprovalViewModel students);
    public Task<bool> ApproveParentsAsync(List<ParentViewModel> parents);
    public Task<bool> ApproveTeacherAsync(TeacherApprovalViewModel approvalModel);
    public Task<bool> ApproveAdminsAsync();
}