using FakeItEasy;
using Microsoft.AspNetCore.Mvc; 
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests;

public class MarkControllerTests
{
    private readonly IMarkService _fakeService;
    private readonly MarkController _controller;
    private readonly IMapper _fakeMapper;
    public MarkControllerTests()
    {
        _fakeService = A.Fake<IMarkService>();
        _fakeMapper = A.Fake<IMapper>();
        _controller = new MarkController(_fakeService, _fakeMapper);
    }

    [Fact]
    public async Task AddMark_ValidMarkViewModel_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var model = new MarkViewModel() { Id = 1 /*, other properties if necessary */ };
        var mark = new Mark() { Id = 1 /*, map other properties from model */ };

        A.CallTo(() => _fakeMapper.MapMark(A<MarkViewModel>.Ignored)).Returns(mark);
        A.CallTo(() => _fakeService.AddMarkAsync(A<Mark>.Ignored)).Returns(mark);

        // Act
        var result = await _controller.AddMark(model);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var resultValue = Assert.IsType<Mark>(createdAtActionResult.Value);
        Assert.Equal(mark.Id, resultValue.Id); // Extend this to other properties as needed
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
        A.CallTo(() => _fakeService.GetMarkByIdAsync(A<int>.Ignored)).Returns(mark);

        var result = await _controller.GetMark(1); // Assuming '1' is a valid ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mark, okResult.Value);
    }

    [Fact]
    public async Task GetMark_NonExistingId_ReturnsNotFoundResult()
    {
        A.CallTo(() => _fakeService.GetMarkByIdAsync(A<int>.Ignored)).Returns((Mark)null);

        var result = await _controller.GetMark(999); // Assuming '999' is an invalid ID

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAllMarks_ReturnsOkObjectResultWithMarks()
    {
        var marks = new List<Mark> { /* populate list with marks */ };
        A.CallTo(() => _fakeService.GetAllMarksAsync()).Returns(marks);

        var result = await _controller.GetAllMarks();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }
    [Fact]
    public async Task GetMarksByStudent_ValidStudentId_ReturnsOkObjectResult()
    {
        var marks = new List<Mark> { /* populate list with marks */ };
        A.CallTo(() => _fakeService.GetMarksByStudentAsync(A<int>.Ignored)).Returns(marks);

        var result = await _controller.GetMarksByStudent(1); // Valid student ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }

    [Fact]
    public async Task GetMarksByStudent_InvalidStudentId_ReturnsEmptyList()
    {
        A.CallTo(() => _fakeService.GetMarksByStudentAsync(A<int>.Ignored)).Returns(new List<Mark>());

        var result = await _controller.GetMarksByStudent(999); // Invalid student ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Empty(okResult.Value as IEnumerable<Mark>);
    }
    [Fact]
    public async Task GetMarksByTeacher_ValidTeacherId_ReturnsOkObjectResult()
    {
        var marks = new List<Mark> { /* populate list with marks */ };
        A.CallTo(() => _fakeService.GetMarksByTeacherAsync(A<int>.Ignored)).Returns(marks);

        var result = await _controller.GetMarksByTeacher(1); // Valid teacher ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }

    [Fact]
    public async Task GetMarksByTeacher_InvalidTeacherId_ReturnsEmptyList()
    {
        A.CallTo(() => _fakeService.GetMarksByTeacherAsync(A<int>.Ignored)).Returns(new List<Mark>());

        var result = await _controller.GetMarksByTeacher(999); // Invalid teacher ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Empty(okResult.Value as IEnumerable<Mark>);
    }
    [Fact]
    public async Task GetMarksBySubject_ValidSubjectId_ReturnsOkObjectResult()
    {
        var marks = new List<Mark> { /* populate list with marks */ };
        A.CallTo(() => _fakeService.GetMarksBySubjectAsync(A<int>.Ignored)).Returns(marks);

        var result = await _controller.GetMarksBySubject(1); // Valid subject ID

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(marks, okResult.Value);
    }
    [Fact]
    public async Task UpdateMark_ValidData_ReturnsOkObjectResult()
    {
        var model = new MarkViewModel(){Id = 1};
        var mark = new Mark() { Id = 1, /* Other properties */ };
        A.CallTo(() => _fakeMapper.MapMark(A<MarkViewModel>.Ignored)).Returns(mark);
        A.CallTo(() => _fakeService.UpdateMarkAsync(A<Mark>.Ignored)).Returns(mark);
        var result = await _controller.UpdateMark(1, model);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mark, okResult.Value);
    }

    [Fact]
    public async Task UpdateMark_IdMismatch_ReturnsBadRequest()
    {
        var mark = new MarkViewModel() { Id = 1, /* Other properties */ };

        var result = await _controller.UpdateMark(2, mark);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMark_NonExistingMark_ReturnsNotFound()
    {
        var mark = new MarkViewModel() { Id = 999, /* Other properties */ };
        A.CallTo(() => _fakeService.UpdateMarkAsync(A<Mark>.That.Matches(m => m.Id == mark.Id))).Returns((Mark)null);

        var result = await _controller.UpdateMark(999, mark);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteMark_ExistingId_ReturnsOkResult()
    {
        A.CallTo(() => _fakeService.DeleteMarkAsync(A<int>.That.IsEqualTo(1))).Returns(true);


        var result = await _controller.DeleteMark(1);

        Assert.IsType<OkResult>(result);
    }


    [Fact]
    public async Task DeleteMark_NonExistingId_ReturnsNotFoundResult()
    {
        A.CallTo(() => _fakeService.DeleteMarkAsync(A<int>.That.IsEqualTo(999))).Throws(new KeyNotFoundException());

        var result = await _controller.DeleteMark(999);

        Assert.IsType<NotFoundResult>(result);
    }
}