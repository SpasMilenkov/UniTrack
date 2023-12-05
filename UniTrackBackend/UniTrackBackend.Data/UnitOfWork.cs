using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Repositories;

namespace UniTrackBackend.Data;

public sealed class UnitOfWork : IDisposable
{
    private readonly UniTrackDbContext _context;
    private EfRepository<Absence>? _absenceRepository;
    private EfRepository<ElectiveSubject>? _electiveSubjectRepository;
    private EfRepository<Grade>? _gradeRepository;
    private EfRepository<Mark>? _markRepository;
    private EfRepository<Parent>? _parentRepository;
    private StudentRepository? _studentRepository;
    private EfRepository<Subject>? _subjectRepository;
    private EfRepository<Teacher>? _teacherRepository;
    private EfRepository<User>? _userRepository;
    private EfRepository<School>? _schoolRepository;
    
    public UnitOfWork(UniTrackDbContext context)
    {
        _context = context;
    }
    public EfRepository<School> SchoolRepository
    {
        get
        {
            _schoolRepository ??= new EfRepository<School>(_context);

            return _schoolRepository;
        }
    }
    public EfRepository<User> UserRepository
    {
        get
        {
            _userRepository ??= new EfRepository<User>(_context);

            return _userRepository;
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

    public StudentRepository StudentRepository
    {
        get
        {
            _studentRepository ??= new StudentRepository(_context);
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

    private void Dispose(bool disposing)
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