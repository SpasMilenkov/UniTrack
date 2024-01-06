using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests;

public class TeacherControllerTests
{
    private readonly ITeacherService _fakeTeacherService;
    private readonly IMapper _fakeMapper;
    private readonly IGradeService _fakeGradeService;
    private readonly TeacherController _controller;

    public TeacherControllerTests()
    {
        _fakeTeacherService = A.Fake<ITeacherService>();
        _fakeMapper = A.Fake<IMapper>();
        _fakeGradeService = A.Fake<IGradeService>();
        _controller = new TeacherController(_fakeTeacherService, _fakeMapper, _fakeGradeService);
    }

    [Fact]
    public async Task GetTeacher_NonExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var teacherId = 1;
        A.CallTo(() => _fakeTeacherService.GetTeacherByIdAsync(teacherId)).Returns(Task.FromResult<Teacher>(null));

        // Act
        var result = await _controller.GetTeacher(teacherId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task UpdateTeacher_ValidData_ReturnsOkResult()
    {
        // Arrange
        var teacherId = 1;
        var teacherDto = new TeacherDto(teacherId, "John", "Doe", 3);
        var updatedTeacher = new Teacher { /* set properties */ };

        A.CallTo(() => _fakeTeacherService.UpdateTeacherAsync(teacherDto, teacherId)).Returns(Task.FromResult(updatedTeacher));

        // Act
        var result = await _controller.UpdateTeacher(teacherId, teacherDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public async Task DeleteTeacher_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var teacherId = 1;

        A.CallTo(() => _fakeTeacherService.DeleteTeacherAsync(teacherId)).DoesNothing();

        // Act
        var result = await _controller.DeleteTeacher(teacherId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}