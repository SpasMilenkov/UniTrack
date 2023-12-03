using System.Security.Cryptography.Pkcs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Mappings;
using UniTrackBackend.Services.StudentService;

namespace UniTrackBackend.Api.Tests;

public class StudentControllerTests
{
    private readonly Mock<IStudentService> _mockService;
    private readonly StudentController _controller;
    private readonly Mock<IMapper> _mockMapper;

    public StudentControllerTests()
    {
        _mockService = new Mock<IStudentService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new StudentController(_mockService.Object, _mockMapper.Object);
    }
    [Fact]
    public async Task GetStudent_ExistingId_ReturnsOkObjectResult()
    {
        var student = new Student { Id = 1, /* Other properties */ };

        _mockService.Setup(s => s.GetStudentByIdAsync(1)).ReturnsAsync(student);
        _mockMapper.Setup(m => m.MapStudent(It.IsAny<StudentViewModel>())).Returns(student);

        var result = await _controller.GetStudent(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task GetStudent_NonExistingId_ReturnsNotFoundResult()
    {
        _mockService.Setup(s => s.GetStudentByIdAsync(999)).ReturnsAsync((Student)null);

        var result = await _controller.GetStudent(999);

        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task GetAllStudents_ReturnsOkObjectResultWithStudents()
    {
        var students = new List<Student> { /* Initialize list of students */ };
        _mockService.Setup(s => s.GetAllStudentsAsync()).ReturnsAsync(students);

        var result = await _controller.GetAllStudents();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(students, okResult.Value);
    }
    [Fact]
    public async Task DeleteStudent_ReturnsNoContentResult()
    {
        var studentId = 1;
        _mockService.Setup(s => s.DeleteStudentAsync(studentId)).ReturnsAsync(true);

        var result = await _controller.DeleteStudent(studentId);

        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task GetAllStudents_NoStudents_ReturnsOkObjectResultWithEmptyList()
    {
        _mockService.Setup(s => s.GetAllStudentsAsync()).ReturnsAsync(new List<Student>());

        var result = await _controller.GetAllStudents();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var students = Assert.IsAssignableFrom<IEnumerable<Student>>(okResult.Value);
        Assert.Empty(students);
    }
    [Fact]
    public async Task DeleteStudent_NonExistingStudent_ReturnsNotFound()
    {
        int nonExistingId = 999;
        _mockService.Setup(s => s.DeleteStudentAsync(nonExistingId))
            .Throws(new KeyNotFoundException());

        var result = await _controller.DeleteStudent(nonExistingId);

        Assert.IsType<NotFoundResult>(result);
    }
}