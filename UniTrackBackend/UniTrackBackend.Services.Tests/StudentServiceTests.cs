using FakeItEasy;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Tests;

public class StudentServiceTests
{
    private readonly StudentService _studentService;
    private readonly IUnitOfWork _unitOfWork;

    public StudentServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _studentService = new StudentService(_unitOfWork);
    }
    [Fact]
    public async Task AddStudentAsync_ValidStudent_ReturnsStudent()
    {
        // Arrange
        var student = new Student { /* Initialize with valid data */ };

        // Act
        var result = await _studentService.AddStudentAsync(student);

        // Assert
        Assert.Equal(student, result);
        A.CallTo(() => _unitOfWork.StudentRepository.AddAsync(student)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveAsync()).MustHaveHappenedOnceExactly();
    }
    [Fact]
    public async Task GetStudentByIdAsync_ValidId_ReturnsStudent()
    {
        // Arrange
        var studentId = 1;
        var student = new Student { /* Initialize with valid data */ };
        A.CallTo(() => _unitOfWork.StudentRepository.GetStudentWithDetailsAsync(studentId)).Returns(student);

        // Act
        var result = await _studentService.GetStudentByIdAsync(studentId);

        // Assert
        Assert.Equal(student, result);
    }

    [Fact]
    public async Task GetStudentByIdAsync_InvalidId_ReturnsNull()
    {
        // Arrange
        var studentId = 1;
        A.CallTo(() => _unitOfWork.StudentRepository.GetStudentWithDetailsAsync(studentId)).Returns((Student)null);

        // Act
        var result = await _studentService.GetStudentByIdAsync(studentId);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task GetAllStudentsAsync_ReturnsListOfStudents()
    {
        // Arrange
        var students = new List<Student> { /* Initialize with valid data */ };
        A.CallTo(() => _unitOfWork.StudentRepository.GetAllAsync()).Returns(students);

        // Act
        var result = await _studentService.GetAllStudentsAsync();

        // Assert
        Assert.Equal(students, result);
    }
    [Fact]
    public async Task DeleteStudentAsync_ValidId_ReturnsTrue()
    {
        // Arrange
        var studentId = 1;
        var student = new Student { /* Initialize with valid data */ };
        A.CallTo(() => _unitOfWork.StudentRepository.GetByIdAsync(studentId)).Returns(student);

        // Act
        var result = await _studentService.DeleteStudentAsync(studentId);

        // Assert
        Assert.True(result);
        A.CallTo(() => _unitOfWork.StudentRepository.DeleteAsync(studentId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteStudentAsync_InvalidId_ReturnsFalse()
    {
        // Arrange
        var studentId = 1;
        A.CallTo(() => _unitOfWork.StudentRepository.GetByIdAsync(studentId)).Returns((Student)null);

        // Act
        var result = await _studentService.DeleteStudentAsync(studentId);

        // Assert
        Assert.False(result);
    }

}
