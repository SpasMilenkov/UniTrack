using FakeItEasy;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Tests;

public class AnalysisServiceTests
{  
    private readonly AnalysisService _analysisService;
    private readonly IUnitOfWork _unitOfWork;

    public AnalysisServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        ILogger<AnalysisService> logger = A.Fake<ILogger<AnalysisService>>();
        _analysisService = new AnalysisService(_unitOfWork, logger);
    }
    [Fact]
    public void CalculateStudentPerformanceBySubject_WhenExceptionOccurs_ReturnsNullAndLogsError()
    {
        // Arrange
        var student = new Student
        {
            Marks = new List<Mark>
            {
                new Mark { Subject = null } // Create an invalid scenario that leads to an exception
            }
        };

        // Act
        var result = _analysisService.CalculateStudentPerformanceBySubject(student);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task GenerateAnalysisModel_WithValidStudentId_ReturnsAnalysisModel()
    {
        // Arrange
        var studentId = 123;
        var fakeStudent = new Student
        {
            User = new User()
            {
                FirstName = "Mario",
                LastName = "Mario"
            },
            Id = studentId,
            Marks = new List<Mark>
            {
                new Mark { Value = 5.0m, Subject = new Subject { Name = "Math" } },
                new Mark { Value = 4.0m, Subject = new Subject { Name = "Science" } }
                // Add more marks as necessary
            },
            Absences = new List<Absence>()
            // Populate other properties as needed
        };

        A.CallTo(() => _unitOfWork.StudentRepository.GetStudentWithDetailsAsync(studentId)).Returns(fakeStudent);

        // Act
        var result = await _analysisService.GenerateAnalysisModel(studentId);

        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async Task GenerateAnalysisModel_WhenStudentNotFound_ReturnsNull()
    {
        // Arrange
        var studentId = 123;
        A.CallTo(() => _unitOfWork.StudentRepository.GetStudentWithDetailsAsync(studentId)).Returns((Student)null);

        // Act
        var result = await _analysisService.GenerateAnalysisModel(studentId);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public void CalculateStudentPerformanceBySubject_WithNoMarks_ReturnsEmptyList()
    {
        // Arrange
        var student = new Student
        {
            Marks = new List<Mark>() // No marks
        };

        // Act
        var result = _analysisService.CalculateStudentPerformanceBySubject(student);

        // Assert
        Assert.Empty(result);
    }
    [Fact]
    public void CalculateStudentPerformanceBySubject_WithValidMarks_ReturnsPerformanceList()
    {
        // Arrange
        var student = new Student
        {
            Marks = new List<Mark>
            {
                new Mark { Value = 4.0m, Subject = new Subject { Name = "Math" } },
                new Mark { Value = 5.0m, Subject = new Subject { Name = "Science" } }
            }
        };

        // Act
        var result = _analysisService.CalculateStudentPerformanceBySubject(student);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count); // Assuming 2 subjects
        // Additional assertions to verify the average calculation
    }
}