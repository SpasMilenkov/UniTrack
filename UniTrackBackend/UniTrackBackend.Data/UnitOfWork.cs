using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Repositories;

namespace UniTrackBackend.Data;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly UniTrackDbContext _context;
    private EfRepository<Absence>? _absenceRepository;
    private EfRepository<Grade>? _gradeRepository;
    private MarkRepository? _markRepository;
    private EfRepository<Parent>? _parentRepository;
    private StudentRepository? _studentRepository;
    private EfRepository<Subject>? _subjectRepository;
    private EfRepository<Teacher>? _teacherRepository;
    private EfRepository<School>? _schoolRepository;
    private EfRepository<Admin>? _adminRepository;
    
    public UnitOfWork(UniTrackDbContext context)
    {
        _context = context;
    }
    public IRepository<School> SchoolRepository
    {
        get
        {
            _schoolRepository ??= new EfRepository<School>(_context);

            return _schoolRepository;
        }
    }

    public IRepository<Absence> AbsenceRepository
    {
        get
        {
            _absenceRepository ??= new EfRepository<Absence>(_context);

            return _absenceRepository;
        }
    }

    public IRepository<Grade> GradeRepository
    {
        get
        {
            _gradeRepository ??= new EfRepository<Grade>(_context);
            return _gradeRepository;
        }
    }

    public IMarkRepository MarkRepository
    {
        get
        {
            _markRepository ??= new MarkRepository(_context);
            return _markRepository;
        }
    }

    public IRepository<Parent> ParentRepository
    {
        get
        {
            _parentRepository ??= new EfRepository<Parent>(_context);
            return _parentRepository;
        }
    }

    public IStudentRepository StudentRepository
    {
        get
        {
            _studentRepository ??= new StudentRepository(_context);
            return _studentRepository;
        }
    }

    public IRepository<Subject> SubjectRepository
    {
        get
        {
            _subjectRepository ??= new EfRepository<Subject>(_context);
            return _subjectRepository;
        }
    }

    public IRepository<Teacher> TeacherRepository
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
    public IRepository<Admin> AdminRepository
    {
        get
        {
            _adminRepository ??= new EfRepository<Admin>(_context);

            return _adminRepository;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}