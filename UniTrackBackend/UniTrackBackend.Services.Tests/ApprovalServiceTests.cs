using System.Linq.Expressions;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Models.TypeSafe;

namespace UniTrackBackend.Services.Tests;

public class ApprovalServiceTests
{
    // private readonly ApprovalService _approvalService;
    // private readonly IUnitOfWork _unitOfWork;
    // private readonly UserManager<User> _userManager;
    // private readonly ILogger<ApprovalService> _logger;
    //
    // public ApprovalServiceTests()
    // {
    //     _unitOfWork = A.Fake<IUnitOfWork>();
    //     _userManager = A.Fake<UserManager<User>>();
    //     _logger = A.Fake<ILogger<ApprovalService>>();
    //     _approvalService = new ApprovalService(_unitOfWork, _userManager, _logger);
    // }
    //
    // [Fact]
    // public async Task ApproveStudentsAsync_AllValid_ReturnsTrue()
    // {
    //     // Arrange
    //     var students = new List<StudentViewModel> { /* Populate with test data */ };
    //     A.CallTo(() => _userManager.FindByEmailAsync(A<string>._)).Returns(new User
    //     {
    //         FirstName = null,
    //         LastName = null
    //     });
    //     A.CallTo(() => _unitOfWork.GradeRepository.SingleOrDefaultAsync(A<Expression<Func<Grade, bool>>>._)).Returns(new Grade());
    //
    //     // Act
    //     var result = await _approvalService.ApproveStudentsAsync(students);
    //
    //     // Assert
    //     Assert.True(result);
    //     A.CallTo(() => _userManager.AddToRoleAsync(A<User>._, Ts.Roles.Student)).MustHaveHappened();
    //     A.CallTo(() => _unitOfWork.StudentRepository.AddAsync(A<Student>._)).MustHaveHappened();
    //     A.CallTo(() => _unitOfWork.SaveAsync()).MustHaveHappened();
    // }
    // [Fact]
    // public async Task ApproveStudentsAsync_UserNotFound_ReturnsFalse()
    // {
    //     // Arrange
    //     var students = new List<StudentViewModel> { /* Populate with test data */ };
    //     A.CallTo(() => _userManager.FindByEmailAsync(A<string>._)).Returns((User)null);
    //
    //     // Act
    //     var result = await _approvalService.ApproveStudentsAsync(students);
    //
    //     // Assert
    //     Assert.False(result);
    // }
    // [Fact]
    // public async Task ApproveStudentsAsync_GradeNotFound_ReturnsFalse()
    // {
    //     // Arrange
    //     var students = new List<StudentViewModel> { /* Populate with test data */ };
    //     A.CallTo(() => _userManager.FindByEmailAsync(A<string>._)).Returns(new User
    //     {
    //         FirstName = null,
    //         LastName = null
    //     });
    //     A.CallTo(() => _unitOfWork.GradeRepository.SingleOrDefaultAsync(A<Expression<Func<Grade, bool>>>._)).Returns((Grade)null);
    //
    //     // Act
    //     var result = await _approvalService.ApproveStudentsAsync(students);
    //
    //     // Assert
    //     Assert.False(result);
    // }
    // [Fact]
    // public async Task ApproveStudentsAsync_WhenExceptionOccurs_ThrowsException()
    // {
    //     // Arrange
    //     var students = new List<StudentViewModel> { /* Populate with test data */ };
    //     A.CallTo(() => _userManager.FindByEmailAsync(A<string>._)).Throws<Exception>();
    //
    //     // Act & Assert
    //     await Assert.ThrowsAsync<Exception>(() => _approvalService.ApproveStudentsAsync(students));
    //     A.CallTo(() => _logger.LogError(A<Exception>._, A<string>._)).MustHaveHappened();
    // }

}
