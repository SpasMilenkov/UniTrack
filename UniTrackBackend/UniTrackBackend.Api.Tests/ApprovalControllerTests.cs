using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Controllers;
using UniTrackBackend.Services;

namespace UniTrackBackend.Api.Tests;

public class ApprovalControllerTests
{
    private readonly IApprovalService _fakeApprovalService;
    private readonly ApprovalController _controller;

    public ApprovalControllerTests()
    {
        _fakeApprovalService = A.Fake<IApprovalService>();
        _controller = new ApprovalController(_fakeApprovalService);
    }
    
    [Fact]
    public async Task ApproveStudents_WhenSuccessful_ReturnsOkResult()
    {
        // Arrange
        var studentApprovalDto = new StudentApprovalDto { /* Initialize properties */ };
        A.CallTo(() => _fakeApprovalService.ApproveStudentsAsync(studentApprovalDto)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.ApproveStudents(studentApprovalDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ApproveStudents_WhenFailed_ReturnsBadRequest()
    {
        // Arrange
        var studentApprovalDto = new StudentApprovalDto { /* Initialize properties */ };
        A.CallTo(() => _fakeApprovalService.ApproveStudentsAsync(studentApprovalDto)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.ApproveStudents(studentApprovalDto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task ApproveTeachers_WhenSuccessful_ReturnsOkResult()
    {
        // Arrange
        var teacherApprovalDto = new TeacherApprovalDto { /* Initialize properties */ };
        A.CallTo(() => _fakeApprovalService.ApproveTeacherAsync(teacherApprovalDto)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.ApproveTeachers(teacherApprovalDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ApproveTeachers_WhenFailed_ReturnsBadRequest()
    {
        // Arrange
        var teacherApprovalDto = new TeacherApprovalDto { /* Initialize properties */ };
        A.CallTo(() => _fakeApprovalService.ApproveTeacherAsync(teacherApprovalDto)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.ApproveTeachers(teacherApprovalDto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task ApproveParents_WhenSuccessful_ReturnsOkResult()
    {
        // Arrange
        var parentDto = new ParentDto { /* Initialize properties */ };
        A.CallTo(() => _fakeApprovalService.ApproveParentsAsync(parentDto)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.ApproveParents(parentDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ApproveParents_WhenFailed_ReturnsBadRequest()
    {
        // Arrange
        var parentDto = new ParentDto { /* Initialize properties */ };
        A.CallTo(() => _fakeApprovalService.ApproveParentsAsync(parentDto)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.ApproveParents(parentDto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}