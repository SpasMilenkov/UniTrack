using Microsoft.AspNetCore.Mvc;
using Moq;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Interfaces;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Api.Tests;

public class MarkControllerTests
{
    private readonly Mock<IMarkService> _mockService;
    private readonly MarkController _controller;

    public MarkControllerTests()
    {
        _mockService = new Mock<IMarkService>();
        _controller = new MarkController(_mockService.Object);
    }
    [Fact]
    public async Task AddMark_ValidMark_ReturnsCreatedAtActionResult()
    {
        var mark = new Mark { /* set properties */ };
        _mockService.Setup(s => s.AddMarkAsync(It.IsAny<Mark>())).ReturnsAsync(mark);

        var result = await _controller.AddMark(mark);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(mark, createdAtActionResult.Value);
    }

    [Fact]
    public async Task AddMark_InvalidMark_ReturnsBadRequest()
    {
        var result = await _controller.AddMark(null); // Invalid mark

        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task GetMark_ExistingId_ReturnsOkObjectResult()
    {
        var mark = new Mark { /* set properties */ };
        _mockService.Setup(s => s.GetMarkByIdAsync(It.IsAny<int>())).ReturnsAsync(mark);

        var result = await _controller.GetMark(1); // Assuming '1' is a valid ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mark, okResult.Value);
    }

    [Fact]
    public async Task GetMark_NonExistingId_ReturnsNotFoundResult()
    {
        _mockService.Setup(s => s.GetMarkByIdAsync(It.IsAny<int>())).ReturnsAsync((Mark)null);

        var result = await _controller.GetMark(999); // Assuming '999' is an invalid ID

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAllMarks_ReturnsOkObjectResultWithMarks()
    {
        var marks = new List<Mark> { /* populate list with marks */ };
        _mockService.Setup(s => s.GetAllMarksAsync()).ReturnsAsync(marks);

        var result = await _controller.GetAllMarks();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }
    [Fact]
    public async Task GetMarksByStudent_ValidStudentId_ReturnsOkObjectResult()
    {
        var marks = new List<Mark> { /* populate list with marks */ };
        _mockService.Setup(s => s.GetMarksByStudentAsync(It.IsAny<int>())).ReturnsAsync(marks);

        var result = await _controller.GetMarksByStudent(1); // Valid student ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }

    [Fact]
    public async Task GetMarksByStudent_InvalidStudentId_ReturnsEmptyList()
    {
        _mockService.Setup(s => s.GetMarksByStudentAsync(It.IsAny<int>())).ReturnsAsync(new List<Mark>());

        var result = await _controller.GetMarksByStudent(999); // Invalid student ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Empty(okResult.Value as IEnumerable<Mark>);
    }
    [Fact]
    public async Task GetMarksByTeacher_ValidTeacherId_ReturnsOkObjectResult()
    {
        var marks = new List<Mark> { /* populate list with marks */ };
        _mockService.Setup(s => s.GetMarksByTeacherAsync(It.IsAny<int>())).ReturnsAsync(marks);

        var result = await _controller.GetMarksByTeacher(1); // Valid teacher ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }

    [Fact]
    public async Task GetMarksByTeacher_InvalidTeacherId_ReturnsEmptyList()
    {
        _mockService.Setup(s => s.GetMarksByTeacherAsync(It.IsAny<int>())).ReturnsAsync(new List<Mark>());

        var result = await _controller.GetMarksByTeacher(999); // Invalid teacher ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Empty(okResult.Value as IEnumerable<Mark>);
    }
    [Fact]
    public async Task GetMarksBySubject_ValidSubjectId_ReturnsOkObjectResult()
    {
        var marks = new List<Mark> { /* populate list with marks */ };
        _mockService.Setup(s => s.GetMarksBySubjectAsync(It.IsAny<int>())).ReturnsAsync(marks);

        var result = await _controller.GetMarksBySubject(1); // Valid subject ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }
    [Fact]
    public async Task UpdateMark_ValidData_ReturnsOkObjectResult()
    {
        var mark = new Mark { Id = 1, /* Other properties */ };
        _mockService.Setup(s => s.UpdateMarkAsync(It.IsAny<Mark>())).ReturnsAsync(mark);

        var result = await _controller.UpdateMark(1, mark);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mark, okResult.Value);
    }

    [Fact]
    public async Task UpdateMark_IdMismatch_ReturnsBadRequest()
    {
        var mark = new Mark { Id = 1, /* Other properties */ };

        var result = await _controller.UpdateMark(2, mark);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateMark_NonExistingMark_ReturnsNotFound()
    {
        var mark = new Mark { Id = 999, /* Other properties */ };
        _mockService.Setup(s => s.UpdateMarkAsync(It.IsAny<Mark>())).ReturnsAsync((Mark)null);

        var result = await _controller.UpdateMark(999, mark);

        Assert.IsType<NotFoundResult>(result);
    }

// DeleteMark Tests
    [Fact]
    public async Task DeleteMark_ExistingId_ReturnsNoContentResult()
    {
        _mockService.Setup(s => s.DeleteMarkAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var result = await _controller.DeleteMark(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteMark_NonExistingId_ReturnsNotFoundResult()
    {
        // Assuming DeleteMarkAsync throws an exception or similar for non-existing IDs
        _mockService.Setup(s => s.DeleteMarkAsync(It.IsAny<int>())).Throws(new KeyNotFoundException());

        var result = await _controller.DeleteMark(999);

        Assert.IsType<NotFoundResult>(result);
    }
}