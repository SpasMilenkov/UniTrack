using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests;

public class MarkControllerTests
{
    private readonly IMarkService _fakeMarkService;
    private readonly IMapper _fakeMapper; // Assuming ICustomMapper is your custom mapper interface
    private readonly MarkController _controller;

    public MarkControllerTests()
    {
        _fakeMarkService = A.Fake<IMarkService>();
        _fakeMapper = A.Fake<IMapper>(); // Create a fake of your custom mapper
        _controller = new MarkController(_fakeMarkService, _fakeMapper);
    }

    [Fact]
    public async Task AddMark_ValidModel_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var model = new MarkDto
        {
            Value = 85.0m, // decimal value
            StudentId = 1234,
            TeacherId = 567,
            SubjectId = 89,
            Topic = "Algebra",
            GradedOn = new DateTime(2024, 1, 8)
        };

        // Assuming Mark is a different entity from MarkDto
        var mark = new Mark
        {
            Value = 85.0m,
            StudentId = 1234,
            TeacherId = 567,
            SubjectId = 89,
            Topic = "Algebra",
            GradedOn = new DateTime(2024, 1, 8)
        };

        var resultDto = new MarkResultDto("85", "S1234", "T567", "Sub89", "Algebra", "2024-01-08", "Mathematics", "John", "Doe");

        A.CallTo(() => _fakeMapper.MapMark(model)).Returns(mark);
        A.CallTo(() => _fakeMarkService.AddMarkAsync(mark)).Returns(Task.FromResult(resultDto));

        // Act
        var result = await _controller.AddMark(model);

        // Assert
        var createdAtResult = Assert.IsType<OkResult>(result);
        Assert.IsType<OkResult>(createdAtResult);
    }


    [Theory]
    [InlineData(null)]
    public async Task AddMark_NullModel_ReturnsBadRequest(MarkDto model)
    {
        // Act
        var result = await _controller.AddMark(model);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task GetMark_ExistingId_ReturnsOkObjectResult()
    {
        // Arrange
        var markId = 1;
        var markDto = new MarkResultDto(Value: "85",
            StudentId: "S1234",
            TeacherId: "T567",
            SubjectId: "Sub89",
            Topic: "Algebra",
            GradedOn: "2024-01-08",
            SubjectName: "Mathematics",
            TeacherFirstName: "John",
            TeacherLastName: "Doe");
        A.CallTo(() => _fakeMarkService.GetMarkByIdAsync(markId)).Returns(Task.FromResult(markDto));
    
        // Act
        var result = await _controller.GetMark(markId);
    
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(markDto, okResult.Value);
    }

    [Fact]
    public async Task GetMark_NonExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var markId = 1;
        A.CallTo(() => _fakeMarkService.GetMarkByIdAsync(markId)).Returns(Task.FromResult<MarkResultDto>(null));

        // Act
        var result = await _controller.GetMark(markId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    public class GetAllMarksTests : MarkControllerTests
    {
        [Fact]
        public async Task GetAllMarks_ReturnsOkObjectResultWithMarks()
        {
            // Arrange
            var marks = new List<MarkResultDto> { /* Populate with test marks */ };
            A.CallTo(() => _fakeMarkService.GetAllMarksAsync()).Returns(Task.FromResult<IEnumerable<MarkResultDto>>(marks));

            // Act
            var result = await _controller.GetAllMarks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(marks, okResult.Value);
        }
    }
    [Fact]
    public async Task GetMarksByStudent_WithValidStudentId_ReturnsOkObjectResult()
    {
        // Arrange
        var studentId = 1;
        var marks = new List<MarkResultDto> { /* Populate with test marks */ };
        A.CallTo(() => _fakeMarkService.GetMarksByStudentAsync(studentId)).Returns(Task.FromResult<IEnumerable<MarkResultDto>>(marks));

        // Act
        var result = await _controller.GetMarksByStudent(studentId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }
    
    [Fact]
    public async Task GetMarksBySubject_WithValidSubjectId_ReturnsOkObjectResult()
    {
        // Arrange
        var subjectId = 1;
        var marks = new List<MarkResultDto> { /* Populate with test marks */ };
        A.CallTo(() => _fakeMarkService.GetMarksBySubjectAsync(subjectId)).Returns(Task.FromResult<IEnumerable<MarkResultDto>>(marks));

        // Act
        var result = await _controller.GetMarksBySubject(subjectId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }
    [Fact]
    public async Task GetMarksByDate_WithValidDate_ReturnsOkObjectResult()
    {
        // Arrange
        var date = DateTime.Now;
        var marks = new List<MarkResultDto> { /* Populate with test marks */ };
        A.CallTo(() => _fakeMarkService.GetMarksByDateAsync(date)).Returns(Task.FromResult<IEnumerable<MarkResultDto>>(marks));

        // Act
        var result = await _controller.GetMarksByDate(date);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }
    [Fact]
    public async Task UpdateMark_WithValidData_ReturnsOkObjectResult()
    {
        // Arrange
        var markId = 1;
        var model = new MarkDto
        {
            Topic = null
        };
        var mark = new Mark { /* set properties */ };
        var updatedDto = new MarkResultDto(Value: "85",
            StudentId: "S1234",
            TeacherId: "T567",
            SubjectId: "Sub89",
            Topic: "Algebra",
            GradedOn: "2024-01-08",
            SubjectName: "Mathematics",
            TeacherFirstName: "John",
            TeacherLastName: "Doe");
    
        A.CallTo(() => _fakeMapper.MapMark(model)).Returns(mark);
        A.CallTo(() => _fakeMarkService.UpdateMarkAsync(mark, markId)).Returns(Task.FromResult(updatedDto));
    
        // Act
        var result = await _controller.UpdateMark(markId, model);
    
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(updatedDto, okResult.Value);
    }

    [Fact]
    public async Task UpdateMark_WithNonExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var markId = 1;
        var model = new MarkDto
        {
            Topic = null
        };
        var mark = new Mark { /* set properties */ };

        A.CallTo(() => _fakeMapper.MapMark(model)).Returns(mark);
        A.CallTo(() => _fakeMarkService.UpdateMarkAsync(mark, markId)).Returns(Task.FromResult<MarkResultDto>(null));

        // Act
        var result = await _controller.UpdateMark(markId, model);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task DeleteMark_WithExistingId_ReturnsOkResult()
    {
        // Arrange
        var markId = 1;
        A.CallTo(() => _fakeMarkService.DeleteMarkAsync(markId)).DoesNothing();

        // Act
        var result = await _controller.DeleteMark(markId);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}