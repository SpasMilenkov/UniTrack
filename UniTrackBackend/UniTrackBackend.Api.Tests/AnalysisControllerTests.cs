using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Services.AnalysisService;

namespace UniTrackBackend.Api.Tests;

public class AnalysisControllerTests
{
    [Fact]
    public async Task GetAnalysis_ValidStudentId_ReturnsAnalysisModel()
    {
        // Arrange
        var fakeService = A.Fake<IAnalysisService>();
        var studentId = 1;
        var analysisModel = new StudentAnalysisResultViewModel
        {
            StudentName = null,
            SubjectAvg = null,
            OverallAverage = 0,
            PerformanceSummary = null,
            DetailedSubjectPerformance = null,
            ClassAverageComparison = null,
            Attendance = null
        };

        A.CallTo(() => fakeService.GenerateAnalysisModel(studentId)).Returns(analysisModel);

        var controller = new AnalysisController(fakeService);

        // Act
        var result = await controller.GetAnalysis(studentId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(analysisModel, okResult.Value);
    }
    [Fact]
    public async Task GetAnalysis_InvalidStudentId_ReturnsBadRequest()
    {
        // Arrange
        var fakeService = A.Fake<IAnalysisService>();
        var invalidStudentId = -1; // Assuming negative IDs are invalid

        A.CallTo(() => fakeService.GenerateAnalysisModel(invalidStudentId)).Returns((StudentAnalysisResultViewModel)null);

        var controller = new AnalysisController(fakeService);

        // Act
        var result = await controller.GetAnalysis(invalidStudentId);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

}