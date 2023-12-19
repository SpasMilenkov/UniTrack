using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests;

public class SchoolControllerTests
{
    [Fact]
    public async Task AddSchool_ValidModel_ReturnsCreatedAtActionResult()
    {
        var fakeService = A.Fake<ISchoolService>();
        var fakeMapper = A.Fake<IMapper>();
        var model = new SchoolDto { /* properties */ };
        var school = new School { /* properties */ };

        A.CallTo(() => fakeMapper.MapSchool(model)).Returns(school);
        A.CallTo(() => fakeService.AddSchoolAsync(school)).Returns(school);

        var controller = new SchoolController(fakeService, fakeMapper);

        var result = await controller.AddSchool(model);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(school, createdAtActionResult.Value);
    }

    [Fact]
    public async Task AddSchool_InvalidModel_ReturnsBadRequest()
    {
        var fakeService = A.Fake<ISchoolService>();
        var fakeMapper = A.Fake<IMapper>();
        var model = new SchoolDto { /* invalid properties */ };

        A.CallTo(() => fakeMapper.MapSchool(model)).Returns(null as School);

        var controller = new SchoolController(fakeService, fakeMapper);

        var result = await controller.AddSchool(model);

        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task GetSchool_ExistingId_ReturnsSchool()
    {
        var fakeService = A.Fake<ISchoolService>();
        var school = new School { Id = 1 /* other properties */ };

        A.CallTo(() => fakeService.GetSchoolByIdAsync(1)).Returns(school);

        var controller = new SchoolController(fakeService, A.Fake<IMapper>());

        var result = await controller.GetSchool(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(school, okResult.Value);
    }

    [Fact]
    public async Task GetSchool_NonExistingId_ReturnsNotFound()
    {
        var fakeService = A.Fake<ISchoolService>();

        A.CallTo(() => fakeService.GetSchoolByIdAsync(999)).Returns((School)null);

        var controller = new SchoolController(fakeService, A.Fake<IMapper>());

        var result = await controller.GetSchool(999);

        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task GetAllSchools_ReturnsAllSchools()
    {
        var fakeService = A.Fake<ISchoolService>();
        var schools = new List<School> { /* some schools */ };

        A.CallTo(() => fakeService.GetAllSchoolsAsync()).Returns(schools);

        var controller = new SchoolController(fakeService, A.Fake<IMapper>());

        var result = await controller.GetAllSchools();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(schools, okResult.Value);
    }
    [Fact]
    public async Task UpdateSchool_ValidData_ReturnsUpdatedSchool()
    {
        var fakeService = A.Fake<ISchoolService>();
        var fakeMapper = A.Fake<IMapper>();
        var model = new SchoolDto { Id = 1 /* other properties */ };
        var school = new School { Id = 1 /* other properties */ };

        A.CallTo(() => fakeMapper.MapSchool(model)).Returns(school);
        A.CallTo(() => fakeService.UpdateSchoolAsync(school)).Returns(school);

        var controller = new SchoolController(fakeService, fakeMapper);

        var result = await controller.UpdateSchool(1, model);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(school, okResult.Value);
    }

    [Fact]
    public async Task UpdateSchool_IdMismatch_ReturnsBadRequest()
    {
        var controller = new SchoolController(A.Fake<ISchoolService>(), A.Fake<IMapper>());
        var model = new SchoolDto { Id = 2 /* other properties */ };

        var result = await controller.UpdateSchool(1, model);

        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public async Task DeleteSchool_ExistingId_ReturnsNoContent()
    {
        var fakeService = A.Fake<ISchoolService>();

        A.CallTo(() => fakeService.DeleteSchoolAsync(1)).Returns(true);

        var controller = new SchoolController(fakeService, A.Fake<IMapper>());

        var result = await controller.DeleteSchool(1);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteSchool_NonExistingId_ReturnsNotFound()
    {
        var fakeService = A.Fake<ISchoolService>();

        A.CallTo(() => fakeService.DeleteSchoolAsync(999)).Throws(new KeyNotFoundException());

        var controller = new SchoolController(fakeService, A.Fake<IMapper>());

        var result = await controller.DeleteSchool(999);

        Assert.IsType<NotFoundResult>(result);
    }

}