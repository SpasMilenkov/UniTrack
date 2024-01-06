using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests;

public class StudentControllerTests
{
    private readonly IStudentService _fakeStudentService;
    private readonly IMapper _fakeMapper;
    private readonly StudentController _controller;

    public StudentControllerTests()
    {
        _fakeStudentService = A.Fake<IStudentService>();
        _fakeMapper = A.Fake<IMapper>();
        _controller = new StudentController(_fakeStudentService, _fakeMapper);
    }
    
    public class GetStudentByUserIdTests : StudentControllerTests
    {
        [Fact]
        public async Task GetStudentByUserId_ExistingUserId_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = "someUserId";
            var student = new Student { /* set properties */ };
            var studentDto = new StudentResultDto { /* set properties */ };

            A.CallTo(() => _fakeStudentService.GetStudentByUserIdAsync(userId)).Returns(Task.FromResult(student));
            A.CallTo(() => _fakeMapper.MapStudentDto(student)).Returns(studentDto);

            // Act
            var result = await _controller.GetStudentByUserId(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(studentDto, okResult.Value);
        }

        [Fact]
        public async Task GetStudentByUserId_NonExistingUserId_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = "someUserId";
            A.CallTo(() => _fakeStudentService.GetStudentByUserIdAsync(userId)).Returns(Task.FromResult<Student>(null));

            // Act
            var result = await _controller.GetStudentByUserId(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    public class GetStudentTests : StudentControllerTests
    {
        [Fact]
        public async Task GetStudent_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var studentId = 1;
            var student = new Student { /* set properties */ };
            var studentDto = new StudentResultDto { /* set properties */ };

            A.CallTo(() => _fakeStudentService.GetStudentByIdAsync(studentId)).Returns(Task.FromResult(student));
            A.CallTo(() => _fakeMapper.MapStudentDto(student)).Returns(studentDto);

            // Act
            var result = await _controller.GetStudent(studentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(studentDto, okResult.Value);
        }

        [Fact]
        public async Task GetStudent_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var studentId = 1;
            A.CallTo(() => _fakeStudentService.GetStudentByIdAsync(studentId)).Returns(Task.FromResult<Student>(null));

            // Act
            var result = await _controller.GetStudent(studentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
    public class GetAllStudentsTests : StudentControllerTests
    {
        [Fact]
        public async Task GetAllStudents_ReturnsOkObjectResultWithStudents()
        {
            // Arrange
            var students = new List<Student> { /* Populate with test students */ };
            A.CallTo(() => _fakeStudentService.GetAllStudentsAsync()).Returns(Task.FromResult<IEnumerable<Student>>(students));

            // Act
            var result = await _controller.GetAllStudents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(students, okResult.Value);
        }
    }
    public class DeleteStudentTests : StudentControllerTests
    {
        [Fact]
        public async Task DeleteStudent_ExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var studentId = 1;
            A.CallTo(() => _fakeStudentService.DeleteStudentAsync(studentId)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.DeleteStudent(studentId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteStudent_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var studentId = 1;
            A.CallTo(() => _fakeStudentService.DeleteStudentAsync(studentId)).Throws<KeyNotFoundException>();

            // Act
            var result = await _controller.DeleteStudent(studentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

}