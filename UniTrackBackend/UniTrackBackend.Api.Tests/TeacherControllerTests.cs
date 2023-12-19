using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests;

public class TeacherControllerTests
{
    [Fact]
    public async Task GetAllTeachers_ReturnsAllTeachers()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var fakeMapper = A.Fake<IMapper>();
        var fakeGradeService = A.Fake<IGradeService>();
        var teachers = new List<Teacher> { /* populate with test data */ };
        var viewModels = new List<TeacherResultDto>();
        A.CallTo(() => fakeService.GetAllTeachersAsync()).Returns(teachers);

        var controller = new TeacherController(fakeService, fakeMapper, fakeGradeService);

        // Act
        var result = await controller.GetAllTeachers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(viewModels, okResult.Value as IEnumerable<TeacherResultDto>);
    }
    [Fact]
    public async Task GetTeacher_ExistingId_ReturnsTeacher()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var fakeMapper = A.Fake<IMapper>();
        var fakeGradeService = A.Fake<IGradeService>();
        var teacher = new Teacher
        {
            Id = 1,
            UserId = "1",
            SchoolId = 1,
        };
        var viewModel = new TeacherResultDto
        {
            Id = "1",
            UniId = null,
            AvatarUrl = null,
            FirstName = null,
            LastName = null,
            Type = null,
            ClassId = null,
            ClassName = null,
            Subjects = null
        };
        A.CallTo(() => fakeService.GetTeacherByIdAsync(1)).Returns(teacher);
        A.CallTo(() => fakeMapper.MapTeacherViewModel(teacher, null, null)).Returns(viewModel);
        
        var controller = new TeacherController(fakeService, fakeMapper, fakeGradeService);

        // Act
        var result = await controller.GetTeacher(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(viewModel, okResult.Value);
    }

    [Fact]
    public async Task GetTeacher_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var fakeMapper = A.Fake<IMapper>();
        var fakeGradeService = A.Fake<IGradeService>();
        A.CallTo(() => fakeService.GetTeacherByIdAsync(999)).Returns((Teacher)null);

        var controller = new TeacherController(fakeService, fakeMapper, fakeGradeService);
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
        var fakeMapper = A.Fake<IMapper>();
        var fakeGradeService = A.Fake<IGradeService>();
        var teacher = new Teacher { /* Initialize properties */ };

        A.CallTo(() => fakeService.AddTeacherAsync(A<Teacher>.Ignored)).Returns(teacher);

        var controller = new TeacherController(fakeService, fakeMapper, fakeGradeService);

        // Act
        var result = await controller.AddTeacher(teacher);
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Teacher added successfully",  okResult.Value);
    }
    [Fact]
    public async Task UpdateTeacher_ValidData_ReturnsNoContent()
    {
        // Arrange
        var fakeService = A.Fake<ITeacherService>();
        var fakeMapper = A.Fake<IMapper>();
        var fakeGradeService = A.Fake<IGradeService>();
        var teacher = new Teacher { Id = 1 /* Other properties */ };

        // Here, we are returning a Task that contains a Teacher object
        A.CallTo(() => fakeService.UpdateTeacherAsync(A<Teacher>.Ignored))
            .Returns(Task.FromResult(teacher)); // Assuming UpdateTeacherAsync returns the updated teacher

        var controller = new TeacherController(fakeService, fakeMapper, fakeGradeService);

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
        var fakeMapper = A.Fake<IMapper>();
        var fakeGradeService = A.Fake<IGradeService>();
        var teacher = new Teacher { Id = 2 /* Other properties */ };

        var controller = new TeacherController(fakeService, fakeMapper, fakeGradeService);

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
        var fakeMapper = A.Fake<IMapper>();
        var fakeGradeService = A.Fake<IGradeService>();
        A.CallTo(() => fakeService.DeleteTeacherAsync(1)).Returns(Task.CompletedTask);

        var controller = new TeacherController(fakeService, fakeMapper, fakeGradeService);

        // Act
        var result = await controller.DeleteTeacher(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

}