using System.Linq.Expressions;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Commons.Exceptions;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Services.Tests;

public class MarkServiceTests
{
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly ILogger<MarkService> _fakeLogger;
    private readonly IMapper _fakeMapper;
    private readonly MarkService _service;

    public MarkServiceTests()
    {
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _fakeLogger = A.Fake<ILogger<MarkService>>();
        _fakeMapper = A.Fake<IMapper>();
        _service = new MarkService(_fakeUnitOfWork, _fakeLogger, _fakeMapper);
    }

    public class AddMarkAsyncTests : MarkServiceTests
    {
        [Fact]
        public async Task AddMarkAsync_ValidMark_ReturnsMarkResultDto()
        {
            // Arrange
            var mark = new Mark
            {
                /* set properties */
            };
            var markResultDto = new MarkResultDto
            {
                Topic = null
            };

            A.CallTo(() => _fakeUnitOfWork.MarkRepository.AddAsync(mark)).DoesNothing();
            A.CallTo(() => _fakeUnitOfWork.SaveAsync()).Returns(Task.CompletedTask);
            A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(markResultDto);

            // Act
            var result = await _service.AddMarkAsync(mark);

            // Assert
            Assert.Equal(markResultDto, result);
        }

        public class GetMarkByIdAsyncTests : MarkServiceTests
        {
            [Fact]
            public async Task GetMarkByIdAsync_ExistingId_ReturnsMarkResultDto()
            {
                // Arrange
                var markId = 1;
                var mark = new Mark
                {
                    /* set properties */
                };
                var markResultDto = new MarkResultDto
                {
                    Topic = null
                };

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetByIdAsync(markId)).Returns(Task.FromResult(mark));
                A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(markResultDto);

                // Act
                var result = await _service.GetMarkByIdAsync(markId);

                // Assert
                Assert.Equal(markResultDto, result);
            }

            [Fact]
            public async Task GetMarkByIdAsync_NonExistingId_ThrowsDataNotFoundException()
            {
                // Arrange
                var markId = 1;

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetByIdAsync(markId))
                    .Returns(Task.FromResult<Mark>(null));

                // Act & Assert
                await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.GetMarkByIdAsync(markId));
            }
        }

        public class GetMarksByTeacherAsyncTests : MarkServiceTests
        {
            [Fact]
            public async Task GetMarksByTeacherAsync_ValidTeacherId_ReturnsMarks()
            {
                // Arrange
                var teacherId = 1;
                var marks = new List<Mark>
                {
                    new Mark
                    {
                        /* Initialize properties */
                    }
                };
                var marksResultDto = marks.Select(m => new MarkResultDto
                {
                    Topic = null
                }).ToList();

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                    .Returns(Task.FromResult<IEnumerable<Mark>>(marks));
                // Setting up each mark to be mapped to a corresponding DTO
                foreach (var mark in marks)
                {
                    A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(marksResultDto.First(m => m.Id == mark.Id));
                }

                // Act
                var result = await _service.GetMarksByTeacherAsync(teacherId);

                // Assert
                Assert.Equal(marksResultDto, result);
            }

            [Fact]
            public async Task GetMarksByTeacherAsync_OnError_ThrowsDataNotFoundException()
            {
                // Arrange
                var teacherId = 1;
                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                    .Throws<Exception>();

                // Act & Assert
                await Assert.ThrowsAsync<DataNotFoundException>(async () =>
                    await _service.GetMarksByTeacherAsync(teacherId));
            }
        }

        public class GetMarksByStudentAsyncTests : MarkServiceTests
        {
            [Fact]
            public async Task GetMarksByStudentAsync_ValidStudentId_ReturnsMarks()
            {
                // Arrange
                var studentId = 1;
                var marks = new List<Mark>
                    { new Mark { Id = 1, Value = 90, StudentId = studentId, TeacherId = 2, SubjectId = 3 } };
                var marksResultDto = marks.Select(m => new MarkResultDto
                {
                    Id = 1, 
                    Value = 90, 
                    StudentId = studentId, 
                    TeacherId = 2, 
                    SubjectId = 3,
                    Topic = null
                }).ToList();

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                    .Returns(Task.FromResult<IEnumerable<Mark>>(marks));
                foreach (var mark in marks)
                {
                    A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(marksResultDto.First(m => m.Id == mark.Id));
                }

                // Act
                var result = await _service.GetMarksByStudentAsync(studentId);

                // Assert
                Assert.Equal(marksResultDto, result);
            }

            [Fact]
            public async Task GetMarksByStudentAsync_OnError_ThrowsDataNotFoundException()
            {
                // Arrange
                var studentId = 1;
                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                    .Throws<Exception>();

                // Act & Assert
                await Assert.ThrowsAsync<DataNotFoundException>(async () =>
                    await _service.GetMarksByStudentAsync(studentId));
            }
        }
        public class GetMarksBySubjectAsyncTests : MarkServiceTests
        {
            [Fact]
            public async Task GetMarksBySubjectAsync_ValidSubjectId_ReturnsMarks()
            {
                // Arrange
                var subjectId = 1;
                var marks = new List<Mark> { new Mark { Id = 1, Value = 88, SubjectId = subjectId, TeacherId = 2, StudentId = 3 } };
                var marksResultDto = marks.Select(m => new MarkResultDto
                {
                    Id = m.Id,
                    Value = m.Value,
                    SubjectId = m.SubjectId,
                    TeacherId = m.TeacherId,
                    StudentId = m.StudentId,
                    Topic = null
                }).ToList();

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                    .Returns(Task.FromResult<IEnumerable<Mark>>(marks));
                foreach (var mark in marks)
                {
                    A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(marksResultDto.First(m => m.Id == mark.Id));
                }

                // Act
                var result = await _service.GetMarksBySubjectAsync(subjectId);

                // Assert
                Assert.Equal(marksResultDto, result);
            }

            [Fact]
            public async Task GetMarksBySubjectAsync_OnError_ThrowsDataNotFoundException()
            {
                // Arrange
                var subjectId = 1;
                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                    .Throws<Exception>();

                // Act & Assert
                await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.GetMarksBySubjectAsync(subjectId));
            }
        }
        public class GetMarksByDateAsyncTests : MarkServiceTests
        {
            [Fact]
            public async Task GetMarksByDateAsync_ValidDate_ReturnsMarks()
            {
                // Arrange
                var date = new DateTime(2021, 04, 05);
                var marks = new List<Mark> { new Mark { Id = 1, Value = 92, GradedOn = date, TeacherId = 2, StudentId = 3 } };
                var marksResultDto = marks.Select(m => new MarkResultDto
                {
                    Id = m.Id,
                    Value = m.Value,
                    GradedOn = m.GradedOn,
                    TeacherId = m.TeacherId,
                    StudentId = m.StudentId,
                    Topic = null
                }).ToList();

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                    .Returns(Task.FromResult<IEnumerable<Mark>>(marks));
                foreach (var mark in marks)
                {
                    A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(marksResultDto.First(m => m.Id == mark.Id));
                }

                // Act
                var result = await _service.GetMarksByDateAsync(date);

                // Assert
                Assert.Equal(marksResultDto, result);
            }

            [Fact]
            public async Task GetMarksByDateAsync_OnError_ThrowsDataNotFoundException()
            {
                // Arrange
                var date = new DateTime(2021, 04, 05);
                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                    .Throws<Exception>();

                // Act & Assert
                await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.GetMarksByDateAsync(date));
            }
        }
        public class UpdateMarkAsyncTests : MarkServiceTests
        {
            [Fact]
            public async Task UpdateMarkAsync_ValidMark_ReturnsUpdatedMarkDto()
            {
                // Arrange
                var markId = 1;
                var mark = new Mark { Id = markId, Value = 95, TeacherId = 2, StudentId = 3 };
                var updatedMarkResultDto = new MarkResultDto
                {
                    Id = mark.Id,
                    Value = mark.Value,
                    TeacherId = mark.TeacherId,
                    StudentId = mark.StudentId,
                    Topic = null
                };

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.UpdateAsync(mark)).DoesNothing();
                A.CallTo(() => _fakeUnitOfWork.SaveAsync()).Returns(Task.CompletedTask);
                A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(updatedMarkResultDto);

                // Act
                var result = await _service.UpdateMarkAsync(mark, markId);

                // Assert
                Assert.Equal(updatedMarkResultDto, result);
            }

            [Fact]
            public async Task UpdateMarkAsync_OnError_ThrowsDataNotFoundException()
            {
                // Arrange
                var markId = 1;
                var mark = new Mark { /* set properties */ };

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.UpdateAsync(mark)).Throws<Exception>();

                // Act & Assert
                await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.UpdateMarkAsync(mark, markId));
            }
        }
        public class DeleteMarkAsyncTests : MarkServiceTests
        {
            [Fact]
            public async Task DeleteMarkAsync_ValidId_DeletesMark()
            {
                // Arrange
                var markId = 1;
                var mark = new Mark { Id = markId };

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetByIdAsync(markId)).Returns(Task.FromResult(mark));
                A.CallTo(() => _fakeUnitOfWork.MarkRepository.DeleteAsync(markId)).DoesNothing();
                A.CallTo(() => _fakeUnitOfWork.SaveAsync()).Returns(Task.CompletedTask);

                // Act
                await _service.DeleteMarkAsync(markId);

                // Assert - no exception is thrown
                A.CallTo(() => _fakeUnitOfWork.MarkRepository.DeleteAsync(markId)).MustHaveHappenedOnceExactly();
            }

            [Fact]
            public async Task DeleteMarkAsync_OnError_ThrowsDataNotFoundException()
            {
                // Arrange
                var markId = 1;

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetByIdAsync(markId)).Throws<Exception>();

                // Act & Assert
                await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.DeleteMarkAsync(markId));
            }
        }

    }
    

}
