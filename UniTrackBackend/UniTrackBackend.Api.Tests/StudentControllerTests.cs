using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests;

public class StudentControllerTests
{
    private readonly IStudentService _fakeService;
    private readonly StudentController _controller;
    private readonly IMapper _fakeMapper;

    public StudentControllerTests()
    {
        _fakeService = A.Fake<IStudentService>();
        _fakeMapper = A.Fake<IMapper>();
        _controller = new StudentController(_fakeService, _fakeMapper);
    }
    [Fact]
    public async Task GetStudent_ExistingId_ReturnsOkObjectResult()
    {
        var student = new Student { Id = 1, /* Other properties */ };

        A.CallTo(() => _fakeService.GetStudentByIdAsync(1)).Returns(student);
        A.CallTo(() => _fakeMapper.MapStudent(A<StudentViewModel>.Ignored)).Returns(student);

        var result = await _controller.GetStudent(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task GetStudent_NonExistingId_ReturnsNotFoundResult()
    {
        A.CallTo(() => _fakeService.GetStudentByIdAsync(999)).Returns((Student)null);

        var result = await _controller.GetStudent(999);

        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task GetAllStudents_ReturnsOkObjectResultWithStudents()
    {
        var students = new List<Student> { /* Initialize list of students */ };
        A.CallTo(() => _fakeService.GetAllStudentsAsync()).Returns(students);

        var result = await _controller.GetAllStudents();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(students, okResult.Value);
    }
    [Fact]
    public async Task DeleteStudent_ReturnsNoContentResult()
    {
        var studentId = 1;
        A.CallTo(() => _fakeService.DeleteStudentAsync(studentId)).Returns(true);
        var result = await _controller.DeleteStudent(studentId);

        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task GetAllStudents_NoStudents_ReturnsOkObjectResultWithEmptyList()
    {
        A.CallTo(() => _fakeService.GetAllStudentsAsync()).Returns(new List<Student>());
        var result = await _controller.GetAllStudents();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var students = Assert.IsAssignableFrom<IEnumerable<Student>>(okResult.Value);
        Assert.Empty(students);
    }
    [Fact]
    public async Task DeleteStudent_NonExistingStudent_ReturnsNotFound()
    {
        const int nonExistingId = 999;
        A.CallTo(() => _fakeService.DeleteStudentAsync(nonExistingId)).Throws(new KeyNotFoundException());
        var result = await _controller.DeleteStudent(nonExistingId);

        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task UpdateStudent_ValidData_ReturnsNoContent()
    {
        var fakeService = A.Fake<IStudentService>();
        var fakeMapper = A.Fake<IMapper>();
        var model = new StudentViewModel
        {
            StudentId = 1,
            Name = null,
            Grade = null,
            Email = null /* Other properties */
        };
        var student = new Student { /* Properties mapped from model */ };

        A.CallTo(() => fakeMapper.MapStudent(model)).Returns(student);
        A.CallTo(() => fakeService.UpdateStudentAsync(student)).Returns(Task.CompletedTask);

        var controller = new StudentController(fakeService, fakeMapper);

        var result = await controller.UpdateStudent(model.StudentId, model);

        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task UpdateStudent_IdMismatch_ReturnsBadRequest()
    {
        var controller = new StudentController(A.Fake<IStudentService>(), A.Fake<IMapper>());
        var model = new StudentViewModel
        {
            StudentId = 2,
            Name = null,
            Grade = null,
            Email = null /* Other properties */
        };

        var result = await controller.UpdateStudent(1, model);

        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task AddStudent_ValidData_ReturnsCreatedAtActionResult()
    {
        var fakeService = A.Fake<IStudentService>();
        var fakeMapper = A.Fake<IMapper>();
        var model = new StudentViewModel
        {
            StudentId = 3,
            Name = null,
            Grade = null,
            Email = null
        };
        var student = new Student { /* Properties mapped from model */ };
        var createdStudent = new Student { Id = 1, /* Other properties */ };

        A.CallTo(() => fakeMapper.MapStudent(model)).Returns(student);
        A.CallTo(() => fakeService.AddStudentAsync(student)).Returns(createdStudent);

        var controller = new StudentController(fakeService, fakeMapper);

        var result = await controller.AddStudent(model);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(createdStudent, createdAtActionResult.Value);
    }

}