using UniTrackBackend.Api.DTO;

namespace UniTrackBackend.Services;

public interface IApprovalService
{
    public Task<bool> ApproveStudentsAsync(StudentApprovalDto students);
    public Task<bool> ApproveParentsAsync(List<ParentDto> parents);
    public Task<bool> ApproveTeacherAsync(TeacherApprovalDto approvalModel);
    public Task<bool> ApproveAdminsAsync();
}