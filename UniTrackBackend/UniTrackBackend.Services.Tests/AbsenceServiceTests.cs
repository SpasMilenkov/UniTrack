using System.Linq.Expressions;
using FakeItEasy;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Tests;

public class AbsenceServiceTests
{
    [Fact]
    public async Task AddAbsenceAsync_AddsAbsenceSuccessfully()
    {
        var fakeContext = A.Fake<IUnitOfWork>();
        var absence = new Absence { /* properties */ };

        A.CallTo(() => fakeContext.AbsenceRepository.AddAsync(absence)).Returns(Task.CompletedTask);

        var service = new AbsenceService(fakeContext);

        var result = await service.AddAbsenceAsync(absence);

        Assert.Equal(absence, result);
    }
    [Fact]
    public async Task GetAbsencesAsync_ReturnsAbsences()
    {
        var fakeContext = A.Fake<IUnitOfWork>();
        var absences = new List<Absence> { /* absence list */ };

        A.CallTo(() => fakeContext.AbsenceRepository.GetAllAsync()).Returns(absences);

        var service = new AbsenceService(fakeContext);

        var result = await service.GetAbsencesAsync();

        Assert.Equal(absences, result);
    }
    [Fact]
    public async Task GetAbsencesByStudentIdAsync_WithValidId_ReturnsAbsences()
    {
        var fakeContext = A.Fake<IUnitOfWork>();
        var studentId = 1;
        var absences = new List<Absence> { /* absence list for the student */ };

        A.CallTo(() => fakeContext.AbsenceRepository.GetAsync(A<Expression<Func<Absence, bool>>>.Ignored, null))
            .Returns(absences);


        var service = new AbsenceService(fakeContext);

        var result = await service.GetAbsencesByStudentIdAsync(studentId);

        Assert.Equal(absences, result);
    }

    [Fact]
    public async Task GetAbsencesByStudentIdAsync_WithInvalidId_ReturnsEmptyList()
    {
        var fakeContext = A.Fake<IUnitOfWork>();
        var invalidStudentId = -1;

        A.CallTo(() => fakeContext.AbsenceRepository.GetAsync(A<Expression<Func<Absence, bool>>>.Ignored, null))
            .Returns(new List<Absence>());
        var service = new AbsenceService(fakeContext);

        var result = await service.GetAbsencesByStudentIdAsync(invalidStudentId);

        Assert.Empty(result);
    }
    [Fact]
    public async Task UpdateAbsenceAsync_WithExistingAbsence_UpdatesSuccessfully()
    {
        var fakeContext = A.Fake<IUnitOfWork>();
        var absenceToUpdate = new Absence { Id = 1, /* other properties */ };

        A.CallTo(() => fakeContext.AbsenceRepository.GetByIdAsync(absenceToUpdate.Id)).Returns(absenceToUpdate);
        A.CallTo(() => fakeContext.AbsenceRepository.UpdateAsync(absenceToUpdate)).Returns(Task.CompletedTask);
        A.CallTo(() => fakeContext.SaveAsync()).Returns(Task.CompletedTask);

        var service = new AbsenceService(fakeContext);

        await service.UpdateAbsenceAsync(absenceToUpdate);

        // No Assert needed if no exception is thrown
    }

    [Fact]
    public async Task UpdateAbsenceAsync_WithNonExistingAbsence_ThrowsException()
    {
        var fakeContext = A.Fake<IUnitOfWork>();
        var nonExistingAbsence = new Absence { Id = -1, /* other properties */ };

        A.CallTo(() => fakeContext.AbsenceRepository.GetByIdAsync(nonExistingAbsence.Id)).Returns((Absence)null);

        var service = new AbsenceService(fakeContext);

        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAbsenceAsync(nonExistingAbsence));
    }
    [Fact]
    public async Task DeleteAbsenceAsync_WithExistingAbsence_DeletesSuccessfully()
    {
        var fakeContext = A.Fake<IUnitOfWork>();
        var absenceId = 1;
        var existingAbsence = new Absence { Id = absenceId, /* other properties */ };

        A.CallTo(() => fakeContext.AbsenceRepository.GetByIdAsync(absenceId)).Returns(existingAbsence);
        A.CallTo(() => fakeContext.AbsenceRepository.DeleteAsync(absenceId)).Returns(Task.CompletedTask);
        A.CallTo(() => fakeContext.SaveAsync()).Returns(Task.CompletedTask);

        var service = new AbsenceService(fakeContext);

        await service.DeleteAbsenceAsync(absenceId);

        // No Assert needed if no exception is thrown
    }

    [Fact]
    public async Task DeleteAbsenceAsync_WithNonExistingAbsence_ThrowsException()
    {
        var fakeContext = A.Fake<IUnitOfWork>();
        var nonExistingAbsenceId = -1;

        A.CallTo(() => fakeContext.AbsenceRepository.GetByIdAsync(nonExistingAbsenceId)).Returns((Absence)null);

        var service = new AbsenceService(fakeContext);

        await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAbsenceAsync(nonExistingAbsenceId));
    }

}