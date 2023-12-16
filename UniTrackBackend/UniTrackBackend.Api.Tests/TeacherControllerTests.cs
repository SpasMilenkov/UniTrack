using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;

namespace UniTrackBackend.Api.Tests;

public class TeacherControllerTests
{
    [Fact]
    public async Task GetAllTeachers_ReturnsAllTeachers()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var teachers = new List<Teacher> { /* populate with test data */ };
        A.CallTo(() => fakeService.GetAllTeachersAsync()).Returns(teachers);

        var controller = new TeacherController(fakeService);

        // Act
        var result = await controller.GetAllTeachers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(teachers, okResult.Value as IEnumerable<Teacher>);
    }
    [Fact]
    public async Task GetTeacher_ExistingId_ReturnsTeacher()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var teacher = new Teacher { Id = 1 /* Other properties */ };
        A.CallTo(() => fakeService.GetTeacherByIdAsync(1)).Returns(teacher);

        var controller = new TeacherController(fakeService);

        // Act
        var result = await controller.GetTeacher(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(teacher, okResult.Value);
    }

    [Fact]
    public async Task GetTeacher_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        A.CallTo(() => fakeService.GetTeacherByIdAsync(999)).Returns((Teacher)null);

        var controller = new TeacherController(fakeService);

        // Act
        var result = await controller.GetTeacher(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task AddTeacher_ValidTeacher_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var teacher = new Teacher { /* Initialize properties */ };
        A.CallTo(() => fakeService.AddTeacherAsync(A<Teacher>.Ignored)).Returns(teacher);

        var controller = new TeacherController(fakeService);

        // Act
        var result = await controller.AddTeacher(teacher);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(teacher, createdAtActionResult.Value);
    }
    [Fact]
    public async Task UpdateTeacher_ValidData_ReturnsNoContent()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var teacher = new Teacher { Id = 1 /* Other properties */ };

        // Here, we are returning a Task that contains a Teacher object
        A.CallTo(() => fakeService.UpdateTeacherAsync(A<Teacher>.Ignored))
            .Returns(Task.FromResult(teacher)); // Assuming UpdateTeacherAsync returns the updated teacher

        var controller = new TeacherController(fakeService);

        // Act
        var result = await controller.UpdateTeacher(1, teacher);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }


    [Fact]
    public async Task UpdateTeacher_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var teacher = new Teacher { Id = 2 /* Other properties */ };

        var controller = new TeacherController(fakeService);

        // Act
        var result = await controller.UpdateTeacher(1, teacher);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task DeleteTeacher_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        A.CallTo(() => fakeService.DeleteTeacherAsync(1)).Returns(Task.CompletedTask);

        var controller = new TeacherController(fakeService);

        // Act
        var result = await controller.DeleteTeacher(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

}