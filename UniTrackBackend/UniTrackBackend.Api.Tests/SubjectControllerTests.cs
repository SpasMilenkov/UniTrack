using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Mappings;
using UniTrackBackend.Services.SubjectService;

namespace UniTrackBackend.Api.Tests;

public class SubjectsControllerTests
{
    private readonly ISubjectService _fakeSubjectService;
    private readonly IMapper _fakeMapper;
    private readonly SubjectsController _controller;

    public SubjectsControllerTests()
    {
        _fakeSubjectService = A.Fake<ISubjectService>();
        _fakeMapper = A.Fake<IMapper>();
        _controller = new SubjectsController(_fakeSubjectService, _fakeMapper);
    }
    [Fact]
    public async Task GetSubject_WithValidId_ReturnsSubject()
    {
        // Arrange
        var fakeSubject = new Subject { /* Initialize subject */ };
        A.CallTo(() => _fakeSubjectService.GetSubjectByIdAsync(1)).Returns(fakeSubject);
        A.CallTo(() => _fakeMapper.MapSubjectResultDto(fakeSubject)).Returns(new SubjectResultDto(
            Id: "S101",
            Name: "Mathematics",
            Teachers: new List<MinimalTeacherResultDto>
            {
                new MinimalTeacherResultDto(Id: "T201", FirstName: "John", LastName: "Smith"),
                new MinimalTeacherResultDto(Id: "T202", FirstName: "Jane", LastName:  "Doe")
            }
        ));

        // Act
        var result = await _controller.GetSubject(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<SubjectResultDto>(okResult.Value);
    }

    [Fact]
    public async Task GetSubject_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        A.CallTo(() => _fakeSubjectService.GetSubjectByIdAsync(1)).Returns((Subject)null);

        // Act
        var result = await _controller.GetSubject(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
    [Fact]
    public async Task AddSubject_ValidSubject_ReturnsCreatedAtAction()
    {
        // Arrange
        var fakeSubjectDto = new SubjectDto
        {
            Name = null,
            TeacherIds = null
        };
        var fakeSubjectResultDto = new SubjectResultDto (
            Id: "S101",
            Name: "Mathematics",
            Teachers: new List<MinimalTeacherResultDto>
            {
                new MinimalTeacherResultDto(Id: "T201", FirstName: "John", LastName: "Smith"),
                new MinimalTeacherResultDto(Id: "T202", FirstName: "Jane", LastName:  "Doe")
            }
        );

        A.CallTo(() => _fakeSubjectService.AddSubjectAsync(fakeSubjectDto)).Returns(new Subject());
        A.CallTo(() => _fakeMapper.MapSubjectResultDto(A<Subject>.Ignored)).Returns(fakeSubjectResultDto);

        // Act
        var result = await _controller.AddSubject(fakeSubjectDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(fakeSubjectResultDto, createdAtActionResult.Value);
    }

    [Fact]
    public async Task AddSubject_InvalidSubject_ReturnsServerError()
    {
        // Arrange
        var fakeSubjectDto = new SubjectDto
        {
            Name = null,
            TeacherIds = null
        };

        A.CallTo(() => _fakeSubjectService.AddSubjectAsync(fakeSubjectDto)).Returns(new Subject());
        A.CallTo(() => _fakeMapper.MapSubjectResultDto(A<Subject>.Ignored)).Returns((SubjectResultDto)null);

        // Act
        var result = await _controller.AddSubject(fakeSubjectDto);

        // Assert
        Assert.IsType<StatusCodeResult>(result.Result);
    }

    [Fact]
    public async Task UpdateSubject_WhenException_ReturnsServerError()
    {
        // Arrange
        var fakeSubjectDto = new SubjectDto
        {
            Name = null,
            TeacherIds = null
        };

        A.CallTo(() => _fakeSubjectService.UpdateSubjectAsync(1, fakeSubjectDto)).Throws<Exception>();

        // Act
        var result = await _controller.UpdateSubject(1, fakeSubjectDto);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
    [Fact]
    public async Task DeleteSubject_ValidSubject_ReturnsNoContent()
    {
        // Arrange
        A.CallTo(() => _fakeSubjectService.DeleteSubjectAsync(1)).DoesNothing();

        // Act
        var result = await _controller.DeleteSubject(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteSubject_WhenException_ReturnsServerError()
    {
        // Arrange
        A.CallTo(() => _fakeSubjectService.DeleteSubjectAsync(1)).Throws<Exception>();

        // Act
        var result = await _controller.DeleteSubject(1);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

}