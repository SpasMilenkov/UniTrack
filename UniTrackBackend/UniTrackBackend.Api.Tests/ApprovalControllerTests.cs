using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Services.ApprovalService;

namespace UniTrackBackend.Api.Tests;

public class ApprovalControllerTests
{
    [Fact]
    public async Task ApproveStudents_ValidData_ReturnsOkResult()
    {
        var fakeService = A.Fake<IApprovalService>();
        var studentModels = new List<StudentViewModel> { /* populate with test data */ };

        A.CallTo(() => fakeService.ApproveStudentsAsync(studentModels)).Returns(true);

        var controller = new ApprovalController(fakeService);

        var result = await controller.ApproveStudents(studentModels);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ApproveStudents_InvalidData_ReturnsBadRequest()
    {
        var fakeService = A.Fake<IApprovalService>();
        var studentModels = new List<StudentViewModel> { /* invalid data */ };

        A.CallTo(() => fakeService.ApproveStudentsAsync(studentModels)).Returns(false);

        var controller = new ApprovalController(fakeService);

        var result = await controller.ApproveStudents(studentModels);

        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task ApproveTeachers_ValidData_ReturnsOkResult()
    {
        var fakeService = A.Fake<IApprovalService>();
        var teacherModels = new List<TeacherViewModel> { /* populate with test data */ };

        A.CallTo(() => fakeService.ApproveTeachersAsync(teacherModels)).Returns(true);

        var controller = new ApprovalController(fakeService);

        var result = await controller.ApproveTeachers(teacherModels);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ApproveTeachers_InvalidData_ReturnsBadRequest()
    {
        var fakeService = A.Fake<IApprovalService>();
        var teacherModels = new List<TeacherViewModel> { /* invalid data */ };

        A.CallTo(() => fakeService.ApproveTeachersAsync(teacherModels)).Returns(false);

        var controller = new ApprovalController(fakeService);

        var result = await controller.ApproveTeachers(teacherModels);

        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task ApproveParents_ValidData_ReturnsOkResult()
    {
        var fakeService = A.Fake<IApprovalService>();
        var parentModels = new List<ParentViewModel> { /* populate with test data */ };

        A.CallTo(() => fakeService.ApproveParentsAsync(parentModels)).Returns(true);

        var controller = new ApprovalController(fakeService);

        var result = await controller.ApproveParents(parentModels);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ApproveParents_InvalidData_ReturnsBadRequest()
    {
        var fakeService = A.Fake<IApprovalService>();
        var parentModels = new List<ParentViewModel> { /* invalid data */ };

        A.CallTo(() => fakeService.ApproveParentsAsync(parentModels)).Returns(false);

        var controller = new ApprovalController(fakeService);

        var result = await controller.ApproveParents(parentModels);

        Assert.IsType<BadRequestResult>(result);
    }

}