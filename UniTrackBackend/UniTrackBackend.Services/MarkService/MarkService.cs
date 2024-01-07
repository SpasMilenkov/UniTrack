using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Commons.Exceptions;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Services
{
    public class MarkService : IMarkService
    {
        private readonly IUnitOfWork _context;
        private readonly ILogger<MarkService> _logger;
        private readonly IMapper _mapper;

        public MarkService(IUnitOfWork context, ILogger<MarkService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddMarkAsync(Mark? mark)
        {
            try
            {
                await _context.MarkRepository.AddAsync(mark);
                await _context.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding mark");
                throw;
            }
        }

        public async Task<MarkResultDto> GetMarkByIdAsync(int id)
        {
            try
            {
                var mark = await _context.MarkRepository.GetByIdAsync(id);
                if (mark == null) throw new ArgumentNullException(nameof(mark));  
                return _mapper.MapMarkDto(mark);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting mark by ID");
                throw new DataNotFoundException();
            }
        }

        public async Task<IEnumerable<MarkResultDto>> GetAllMarksAsync()
        {
            try
            {
                var marks = await _context.MarkRepository.GetAllAsync() as List<Mark>;
                if (marks == null) throw new ArgumentNullException(nameof(marks));  

                return marks.Select(m => _mapper.MapMarkDto(m));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks");
                throw new DataNotFoundException();
            }
                
        }

        public async Task<IEnumerable<MarkResultDto>> GetMarksByStudentAsync(int studentId)
        {
            try
            {
                var marks = await _context.MarkRepository.GetMarksWithDetailsByStudent(studentId);
                if (marks == null) throw new ArgumentNullException(nameof(marks));  

                return marks.Select(m => _mapper.MapMarkDto(m));

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks a student has");
                throw new DataNotFoundException();
            }
        }

        public async Task<IEnumerable<MarkResultDto>> GetMarksByTeacherAsync(int teacherId)
        {
            try
            {
                var marks = await _context.MarkRepository.GetMarksWithDetailsByTeacher(teacherId);
                if (marks == null) throw new ArgumentNullException(nameof(marks));  

                return marks.Select(m => _mapper.MapMarkDto(m));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks a teacher has graded");
                throw new DataNotFoundException();
            }
                
        }

        public async Task<IEnumerable<MarkResultDto>> GetMarksBySubjectAsync(int subjectId)
        {
            try
            {
                var marks = await _context.MarkRepository.GetAsync(m => m.Subject.Id == subjectId);
                if (marks == null) throw new ArgumentNullException(nameof(marks));  

                return marks.Select(m => _mapper.MapMarkDto(m));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks of a current subject");
                throw new DataNotFoundException();
            }
        }

        public async Task<IEnumerable<MarkResultDto>> GetMarksByDateAsync(DateTime date)
        {
            try
            {
                var marks = await _context.MarkRepository.GetAsync(m => m.GradedOn.Date == date.Date);
                
                if (marks == null) throw new ArgumentNullException(nameof(marks));  

                return marks.Select(m => _mapper.MapMarkDto(m));

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get all marks graded in the same day");
                throw new DataNotFoundException();
            }
        }

        public async Task<MarkResultDto> UpdateMarkAsync(Mark mark, int markId)
        {
            try
            {
                mark.Id = markId;
                await _context.MarkRepository.UpdateAsync(mark);
                await _context.SaveAsync();
                return _mapper.MapMarkDto(mark);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to update mark");
                throw new DataNotFoundException();
            }
        }

        public async Task DeleteMarkAsync(int id)
        {
            try
            {
                var mark = await _context.MarkRepository.GetByIdAsync(id);
                if (mark == null) throw new ArgumentNullException(nameof(mark));  
                await _context.MarkRepository.DeleteAsync(id);
                await _context.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to delete mark");
                throw new DataNotFoundException();
            }

        }
    }
}
