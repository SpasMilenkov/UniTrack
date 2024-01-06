using UniTrackBackend.Api.DTO;

namespace UniTrackBackend.Services;

public interface IApprovalService
{
    public Task<bool> ApproveStudentsAsync(StudentApprovalDto students);
    Task<bool> ApproveParentsAsync(ParentDto approvalModel);
    public Task<bool> ApproveTeacherAsync(TeacherApprovalDto approvalModel);

}