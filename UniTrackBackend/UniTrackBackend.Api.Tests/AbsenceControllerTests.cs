using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests;

public class AbsenceControllerTests
{
    [Fact]
    public void Constructor_WithValidDependencies_ShouldCreateInstance()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        var mapper = A.Fake<IMapper>();

        // Act
        var controller = new AbsencesController(absenceService, mapper);

        // Assert
        Assert.NotNull(controller);
    }
    [Fact]
    public async Task PostAbsence_WithServiceException_ShouldHandleException()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        var mapper = A.Fake<IMapper>();
        var absence = new AbsenceDto { /* Populate with valid data */ };
        A.CallTo(() => mapper.MapAbsence(absence)).Throws<Exception>();

        var controller = new AbsencesController(absenceService, mapper);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => controller.PostAbsence(absence));
    }
    [Fact]
    public async Task UpdateAbsence_WithValidData_ShouldReturnNoContentResult()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        var mapper = A.Fake<IMapper>();
        var absence = new AbsenceDto { /* Populate with valid data */ };
        var mappedEntity = new Absence { /* ... */ };
        A.CallTo(() => mapper.MapAbsence(absence)).Returns(mappedEntity);
        A.CallTo(() => absenceService.UpdateAbsenceAsync(mappedEntity)).Returns(Task.CompletedTask);

        var controller = new AbsencesController(absenceService, mapper);

        // Act
        var result = await controller.UpdateAbsence(1, absence);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task DeleteAbsence_WithValidId_ShouldReturnNoContentResult()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        A.CallTo(() => absenceService.DeleteAbsenceAsync(1)).Returns(Task.CompletedTask);

        var controller = new AbsencesController(absenceService, null);

        // Act
        var result = await controller.DeleteAbsence(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task GetAllAbsences_WithServiceException_ShouldHandleException()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        A.CallTo(() => absenceService.GetAbsencesAsync()).Throws<Exception>();

        var controller = new AbsencesController(absenceService, null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => controller.GetAllAbsences());
    }
    [Fact]
    public async Task GetAbsenceByStudentId_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        A.CallTo(() => absenceService.GetAbsencesByStudentIdAsync(A<int>.Ignored)).Returns(new List<Absence>());

        var controller = new AbsencesController(absenceService, null);

        // Act
        var result = await controller.GetAbsenceByStudentId(-1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    [Fact]
    public async Task GetAbsenceByTeacherId_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        A.CallTo(() => absenceService.GetAbsencesByTeacherIdAsync(A<int>.Ignored)).Returns(new List<Absence>());

        var controller = new AbsencesController(absenceService, null);

        // Act
        var result = await controller.GetAbsenceByTeacherId(-1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    [Fact]
    public async Task UpdateAbsence_WithNonExistingId_ShouldReturnBadRequest()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        var mapper = A.Fake<IMapper>();
        var absence = new AbsenceDto { /* Populate with valid data */ };
        var mappedEntity = new Absence { /* ... */ };
        A.CallTo(() => mapper.MapAbsence(absence)).Returns(mappedEntity);
        A.CallTo(() => absenceService.UpdateAbsenceAsync(mappedEntity)).Throws<Exception>();

        var controller = new AbsencesController(absenceService, mapper);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => controller.UpdateAbsence(-1, absence));
    }
    [Fact]
    public async Task DeleteAbsence_WithNonExistingId_ShouldReturnNotFound()
    {
        // Arrange
        var absenceService = A.Fake<IAbsenceService>();
        A.CallTo(() => absenceService.DeleteAbsenceAsync(A<int>.Ignored)).Throws<Exception>();

        var controller = new AbsencesController(absenceService, null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => controller.DeleteAbsence(-1));
    }

}