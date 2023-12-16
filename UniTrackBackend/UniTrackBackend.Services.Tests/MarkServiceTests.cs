using System.Linq.Expressions;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Tests;

public class MarkServiceTests
{
    private readonly MarkService _markService;
    private readonly IUnitOfWork _unitOfWork;

    public MarkServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        var logger = A.Fake<ILogger<MarkService>>();
        _markService = new MarkService(_unitOfWork, logger);
    }
    [Fact]
    public async Task AddMarkAsync_ValidMark_ReturnsMark()
    {
        // Arrange
        var mark = new Mark { /* Initialize with valid data */ };

        // Act
        var result = await _markService.AddMarkAsync(mark);

        // Assert
        Assert.Equal(mark, result);
        A.CallTo(() => _unitOfWork.MarkRepository.AddAsync(mark)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AddMarkAsync_WhenExceptionOccurs_ReturnsNullAndLogsError()
    {
        // Arrange
        var mark = new Mark { /* Initialize with valid data */ };
        A.CallTo(() => _unitOfWork.MarkRepository.AddAsync(mark)).Throws<Exception>();

        // Act
        var result = await _markService.AddMarkAsync(mark);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task GetMarkByIdAsync_ValidId_ReturnsMark()
    {
        // Arrange
        var markId = 1;
        var mark = new Mark { /* Initialize with valid data */ };
        A.CallTo(() => _unitOfWork.MarkRepository.GetByIdAsync(markId)).Returns(mark);

        // Act
        var result = await _markService.GetMarkByIdAsync(markId);

        // Assert
        Assert.Equal(mark, result);
    }

    [Fact]
    public async Task GetMarkByIdAsync_WhenExceptionOccurs_ReturnsNullAndLogsError()
    {
        // Arrange
        var markId = 1;
        A.CallTo(() => _unitOfWork.MarkRepository.GetByIdAsync(markId)).Throws<Exception>();

        // Act
        var result = await _markService.GetMarkByIdAsync(markId);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task GetAllMarksAsync_ReturnsMarks()
    {
        // Arrange
        var marks = new List<Mark> { /* Initialize with valid data */ };
        A.CallTo(() => _unitOfWork.MarkRepository.GetAllAsync()).Returns(marks);

        // Act
        var result = await _markService.GetAllMarksAsync();

        // Assert
        Assert.Equal(marks, result);
    }

    [Fact]
    public async Task GetAllMarksAsync_WhenExceptionOccurs_ReturnsNullAndLogsError()
    {
        // Arrange
        A.CallTo(() => _unitOfWork.MarkRepository.GetAllAsync()).Throws<Exception>();

        // Act
        var result = await _markService.GetAllMarksAsync();

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task UpdateMarkAsync_ValidMark_ReturnsUpdatedMark()
    {
        // Arrange
        var mark = new Mark { /* Initialize with valid data */ };

        // Act
        var result = await _markService.UpdateMarkAsync(mark);

        // Assert
        Assert.Equal(mark, result);
        A.CallTo(() => _unitOfWork.MarkRepository.UpdateAsync(mark)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateMarkAsync_WhenExceptionOccurs_ReturnsNullAndLogsError()
    {
        // Arrange
        var mark = new Mark { /* Initialize with valid data */ };
        A.CallTo(() => _unitOfWork.MarkRepository.UpdateAsync(mark)).Throws<Exception>();

        // Act
        var result = await _markService.UpdateMarkAsync(mark);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task DeleteMarkAsync_ValidId_ReturnsTrue()
    {
        // Arrange
        var markId = 1;
        var mark = new Mark { /* Initialize with valid data */ };
        A.CallTo(() => _unitOfWork.MarkRepository.GetByIdAsync(markId)).Returns(mark);

        // Act
        var result = await _markService.DeleteMarkAsync(markId);

        // Assert
        Assert.True(result);
        A.CallTo(() => _unitOfWork.MarkRepository.DeleteAsync(markId)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteMarkAsync_InvalidId_ReturnsFalse()
    {
        // Arrange
        var markId = 1;
        A.CallTo(() => _unitOfWork.MarkRepository.GetByIdAsync(markId)).Returns((Mark)null);

        // Act
        var result = await _markService.DeleteMarkAsync(markId);

        // Assert
        Assert.False(result);
    }

}
