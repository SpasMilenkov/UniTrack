using Microsoft.Extensions.Logging;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Repositories;

namespace UniTrackBackend.Data;

public class UnitOfWork : IDisposable
{
    private readonly UniTrackDbContext _context;
    private EfRepository<Absence>? _absenceRepository;
    private EfRepository<ElectiveSubject>? _electiveSubjectRepository;
    private EfRepository<Grade>? _gradeRepository;
    private EfRepository<Mark>? _markRepository;
    private EfRepository<Parent>? _parentRepository;
    private EfRepository<Student>? _studentRepository;
    private EfRepository<Subject>? _subjectRepository;
    private EfRepository<Teacher>? _teacherRepository;
    private EfRepository<ScheduleEvent>? _eventRepository;
    private EfRepository<UserSchedule>? _scheduleRepository;
    private EfRepository<Notification>? _notificationRepository;

    public UnitOfWork(UniTrackDbContext context)
    {
        _context = context;
    }
    public EfRepository<Notification> NotificationRepository
    {
        get
        {
            if (_notificationRepository == null)
            {
                _notificationRepository = new EfRepository<Notification>(_context);
            }
            return _notificationRepository;
        }
    }

    public EfRepository<ScheduleEvent> EventRepository
    {
        get
        {
            if (_eventRepository == null)
            {
                _eventRepository = new EfRepository<ScheduleEvent>(_context);
            }
            return _eventRepository;
        }
    }
    public EfRepository<UserSchedule> ScheduleRepository
    {
        get
        {
            if (_scheduleRepository == null)
            {
                _scheduleRepository = new EfRepository<UserSchedule>(_context);
            }
            return _scheduleRepository;
        }
    }

    public EfRepository<Absence> AbsenceRepository
    {
        get
        {
            _absenceRepository ??= new EfRepository<Absence>(_context);

            return _absenceRepository;
        }
    }

    public EfRepository<ElectiveSubject> ElectiveSubjectRepository
    {
        get
        {
            _electiveSubjectRepository ??= new EfRepository<ElectiveSubject>(_context);

            return _electiveSubjectRepository;
        }
    }

    public EfRepository<Grade> GradeRepository
    {
        get
        {
            _gradeRepository ??= new EfRepository<Grade>(_context);
            return _gradeRepository;
        }
    }

    public EfRepository<Mark> MarkRepository
    {
        get
        {
            _markRepository ??= new EfRepository<Mark>(_context);
            return _markRepository;
        }
    }

    public EfRepository<Parent> ParentRepository
    {
        get
        {
            _parentRepository ??= new EfRepository<Parent>(_context);
            return _parentRepository;
        }
    }

    public EfRepository<Student> StudentRepository
    {
        get
        {
            _studentRepository ??= new EfRepository<Student>(_context);
            return _studentRepository;
        }
    }

    public EfRepository<Subject> SubjectRepository
    {
        get
        {
            _subjectRepository ??= new EfRepository<Subject>(_context);
            return _subjectRepository;
        }
    }

    public EfRepository<Teacher> TeacherRepository
    {
        get
        {
            _teacherRepository ??= new EfRepository<Teacher>(_context);
            return _teacherRepository;
        }
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}