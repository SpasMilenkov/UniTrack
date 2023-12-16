using FakeItEasy;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Tests;

public class TeacherServiceTests
{
    private readonly TeacherService _teacherService;
    private readonly IUnitOfWork _unitOfWork;

    public TeacherServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _teacherService = new TeacherService(_unitOfWork);
    }
    [Fact]
    public async Task GetAllTeachersAsync_ReturnsListOfTeachers()
    {
        // Arrange
        var teachers = new List<Teacher>();
        A.CallTo(() => _unitOfWork.TeacherRepository.GetAllAsync()).Returns(teachers);

        // Act
        var result = await _teacherService.GetAllTeachersAsync();

        // Assert
        Assert.Equal(teachers, result);
    }
    [Fact]
    public async Task GetTeacherByIdAsync_ValidId_ReturnsTeacher()
    {
        // Arrange
        var teacherId = 1;
        var teacher = new Teacher();
        A.CallTo(() => _unitOfWork.TeacherRepository.GetByIdAsync(teacherId)).Returns(teacher);

        // Act
        var result = await _teacherService.GetTeacherByIdAsync(teacherId);

        // Assert
        Assert.Equal(teacher, result);
    }
    [Fact]
    public async Task AddTeacherAsync_ValidTeacher_ReturnsTeacher()
    {
        // Arrange
        var teacher = new Teacher();

        // Act
        var result = await _teacherService.AddTeacherAsync(teacher);

        // Assert
        Assert.Equal(teacher, result);
        A.CallTo(() => _unitOfWork.TeacherRepository.AddAsync(teacher)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveAsync()).MustHaveHappenedOnceExactly();
    }
    [Fact]
    public async Task DeleteTeacherAsync_ValidId_DeletesTeacher()
    {
        // Arrange
        var teacherId = 1;
        var teacher = new Teacher {Id = 1};
        A.CallTo(() => _unitOfWork.TeacherRepository.GetByIdAsync(teacherId)).Returns(teacher);

        // Act
        await _teacherService.DeleteTeacherAsync(teacherId);

        // Assert
        A.CallTo(() => _unitOfWork.TeacherRepository.DeleteAsync(teacherId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteTeacherAsync_InvalidId_DoesNotDelete()
    {
        // Arrange
        var teacherId = 1;
        A.CallTo(() => _unitOfWork.TeacherRepository.GetByIdAsync(teacherId)).Returns((Teacher)null);

        // Act
        await _teacherService.DeleteTeacherAsync(teacherId);

        // Assert
        A.CallTo(() => _unitOfWork.TeacherRepository.DeleteAsync(teacherId)).MustNotHaveHappened();
    }
}
