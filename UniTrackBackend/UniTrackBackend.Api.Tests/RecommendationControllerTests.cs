using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Services.RecommendationService;

namespace UniTrackBackend.Api.Tests;

public class RecommendationControllerTests
{
    [Fact]
    public async Task GetAnalysis_ValidStudentId_ReturnsRecommendations()
    {
        // Arrange
        var fakeService = A.Fake<IRecommendationService>();
        var studentId = 1;
        var recommendations = new List<RecommendationResultViewModel> { /* Populate with test data */ };

        A.CallTo(() => fakeService.GetRecommendations(studentId)).Returns(recommendations);

        var controller = new RecommendationController(fakeService);

        // Act
        var result = await controller.GetAnalysis(studentId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(recommendations, okResult.Value);
    }
    [Fact]
    public async Task GetAnalysis_InvalidStudentId_ReturnsBadRequest()
    {
        // Arrange
        var fakeService = A.Fake<IRecommendationService>();
        var invalidStudentId = -1; // Assuming negative IDs are invalid

        A.CallTo(() => fakeService.GetRecommendations(invalidStudentId)).Returns((List<RecommendationResultViewModel>)null);

        var controller = new RecommendationController(fakeService);

        // Act
        var result = await controller.GetAnalysis(invalidStudentId);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

}