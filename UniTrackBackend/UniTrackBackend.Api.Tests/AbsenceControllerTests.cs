using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.AbsenceService;

namespace UniTrackBackend.Api.Tests;

public class AbsenceControllerTests
{
    [Fact]
    public async Task PostAbsence_ValidAbsence_ReturnsCreatedAtActionResult()
    {
        var fakeService = A.Fake<IAbsenceService>();
        var absence = new Absence { /* properties */ };
        A.CallTo(() => fakeService.AddAbsenceAsync(A<Absence>.Ignored)).Returns(absence);

        var controller = new AbsencesController(fakeService);

        var result = await controller.PostAbsence(absence);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(absence, createdAtActionResult.Value);
    }
    [Fact]
    public async Task GetAbsenceById_ExistingId_ReturnsAbsence()
    {
        var fakeService = A.Fake<IAbsenceService>();
        var absence = new Absence { Id = 1 /* other properties */ };
        var absences = new List<Absence> { absence }; // Wrap the absence in a list

        // Ensure the method returns an IEnumerable of Absence
        A.CallTo(() => fakeService.GetAbsencesByStudentIdAsync(1)).Returns(absences);

        var controller = new AbsencesController(fakeService);

        var result = await controller.GetAbsenceById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAbsences = Assert.IsType<List<Absence>>(okResult.Value);
        Assert.Contains(absence, returnedAbsences);
    }

    [Fact]
    public async Task GetAbsenceById_ExistingStudentWithAbsences_ReturnsAbsences()
    {
        var fakeService = A.Fake<IAbsenceService>();
        var absences = new List<Absence> { new Absence { Id = 1 /* other properties */ } };
    
        A.CallTo(() => fakeService.GetAbsencesByStudentIdAsync(1)).Returns(absences);

        var controller = new AbsencesController(fakeService);

        var result = await controller.GetAbsenceById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Assert.IsType<List<Absence>>(okResult.Value);
        Assert.Equal(absences, resultValue);
    }
    [Fact]
    public async Task GetAbsenceById_ExistingStudentWithNoAbsences_ReturnsNoAbsencesFoundMessage()
    {
        var fakeService = A.Fake<IAbsenceService>();
    
        A.CallTo(() => fakeService.GetAbsencesByStudentIdAsync(2)).Returns(new List<Absence>());

        var controller = new AbsencesController(fakeService);

        var result = await controller.GetAbsenceById(2);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("No absences found", okResult.Value);
    }
    [Fact]
    public async Task GetAbsenceById_NonExistingStudent_ReturnsNotFound()
    {
        var fakeService = A.Fake<IAbsenceService>();
    
        A.CallTo(() => fakeService.GetAbsencesByStudentIdAsync(999)).Returns((List<Absence>)null);

        var controller = new AbsencesController(fakeService);

        var result = await controller.GetAbsenceById(999);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("Student not found", notFoundResult.Value);
    }


    [Fact]
    public async Task UpdateAbsence_ValidData_ReturnsNoContent()
    {
        var fakeService = A.Fake<IAbsenceService>();
        var absence = new Absence { Id = 1 /* other properties */ };
        A.CallTo(() => fakeService.UpdateAbsenceAsync(A<Absence>.Ignored)).Returns(Task.CompletedTask);

        var controller = new AbsencesController(fakeService);

        var result = await controller.UpdateAbsence(1, absence);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateAbsence_IdMismatch_ReturnsBadRequest()
    {
        var fakeService = A.Fake<IAbsenceService>();
        var absence = new Absence { Id = 2 /* other properties */ };

        var controller = new AbsencesController(fakeService);

        var result = await controller.UpdateAbsence(1, absence);

        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public async Task DeleteAbsence_ExistingId_ReturnsNoContent()
    {
        var fakeService = A.Fake<IAbsenceService>();
        A.CallTo(() => fakeService.DeleteAbsenceAsync(1)).Returns(Task.CompletedTask);

        var controller = new AbsencesController(fakeService);

        var result = await controller.DeleteAbsence(1);

        Assert.IsType<NoContentResult>(result);
    }

}