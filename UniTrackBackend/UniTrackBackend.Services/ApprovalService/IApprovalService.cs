using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Api.ViewModels.ResultViewModels;

namespace UniTrackBackend.Services.ApprovalService;

public interface IApprovalService
{
    public Task<bool> ApproveStudentsAsync(List<StudentViewModel> students);
    public Task<bool> ApproveParentsAsync(List<ParentViewModel> parents);
    public Task<bool> ApproveTeachersAsync(List<TeacherViewModel> teachers);
}