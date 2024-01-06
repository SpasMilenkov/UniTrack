using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Models.TypeSafe;

namespace UniTrackBackend.Data.Seeding;

#region Class Explanation
// Namespaces and Dependencies
//
// Microsoft.AspNetCore.Identity: For managing users and roles in the application.
// Microsoft.Extensions.DependencyInjection: To inject services into the application.
// UniTrackBackend.Data.Models: Custom data models representing entities like User, Teacher, Student, etc.
// UniTrackBackend.Data.Models.TypeSafe: For type-safe operations, containing constants.


//SeedData Method: The entry point for seeding data.
//It creates a scope from the provided IServiceProvider and
//retrieves necessary services like RoleManager, UnitOfWork, and UserManager.

//Seeding Methods

// SeedRolesAsync: Creates predefined roles (like SuperAdmin, Admin, Teacher, etc.) if they don't exist.
// SeedUsersAsync: Creates a list of users and adds them to the system.
// SeedTeachersAsync: Associates certain users as teachers.
// SeedGradesAsync: Adds grade entities, potentially assigning class teachers.
// SeedStudentsAsync: Registers students, associating them with grades.
// SeedSubjectsAsync: Adds subjects to the system.
// SeedMarksAsync: Creates mark entities for students in various subjects.
// SeedAbsencesAsync: Records absences for students in subjects.
#endregion

public static class DataSeeder
{
    private static UserManager<User> _userManager = null!;
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        _userManager = userManager;
        
        await SeedUsersAsync(roleManager);
        await SeedRolesAsync(roleManager);
        await SeedSchoolsAsync(unitOfWork: unitOfWork);
        await SeedTeachersAsync(unitOfWork);
        await SeedGradesAsync(unitOfWork);
        await SeedStudentsAsync(unitOfWork);
        await SeedSubjectsAsync(unitOfWork);
        await SeedMarksAsync(unitOfWork);
        await SeedAbsencesAsync(unitOfWork);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.SuperAdmin);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Admin);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Guest);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Teacher);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Student);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Parent);
    }

    private static async Task EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    private static async Task SeedUsersAsync(RoleManager<IdentityRole> roleManager)
    {
        var users = new List<User>
        {
            new User { UserName = "user1", FirstName = "John", LastName = "Doe", Email = "user98@example.com", AvatarUrl = "example"},
            new User { UserName = "user2", FirstName = "John", LastName = "Doe", Email = "user97@example.com", AvatarUrl = "example"},
            new User { UserName = "user3", FirstName = "John", LastName = "Doe", Email = "user96@example.com", AvatarUrl = "example"},
            new User { UserName = "user4", FirstName = "John", LastName = "Doe", Email = "user95@example.com", AvatarUrl = "example"},
            new User { UserName = "user5", FirstName = "John", LastName = "Doe", Email = "user94@example.com", AvatarUrl = "example"},
        };
        foreach (var user in users)
        {
            if (await _userManager.FindByEmailAsync(user.Email!) == null)
            {
                await _userManager.CreateAsync(user, "String123!");

            }
        }
    }

    private static async Task SeedTeachersAsync(IUnitOfWork unitOfWork)
    {
        var user = await _userManager.FindByEmailAsync("user98@example.com");
        var user2 = await _userManager.FindByEmailAsync("user97@example.com");
        var school = await unitOfWork.SchoolRepository.FirstOrDefaultAsync(s => s.Name == "Eg Bertolt Brecht");
        await _userManager.AddToRoleAsync(user, Ts.Roles.Teacher);
        await _userManager.AddToRoleAsync(user2, Ts.Roles.Teacher);
        if (school is null)
        {
            await SeedSchoolsAsync(unitOfWork);
            school = await unitOfWork.SchoolRepository.FirstOrDefaultAsync(s => s.Name == "Eg Bertolt Brecht");
        }

        var subjects = await unitOfWork.SubjectRepository.GetAllAsync();
        var enumerable = subjects.ToList();
        if (subjects is null || enumerable.Any())
        {
            await SeedSubjectsAsync(unitOfWork);
            subjects = await unitOfWork.SubjectRepository.GetAllAsync();
            enumerable = subjects.ToList();
        }

        var grades = await unitOfWork.GradeRepository.GetAllAsync();
        var gradesEnumerable = grades.ToList();
        if (grades is null || !gradesEnumerable.Any())
        {
            await SeedGradesAsync(unitOfWork);
            grades = await unitOfWork.GradeRepository.GetAllAsync();
            gradesEnumerable = grades.ToList();
        }

        var teachers = new List<Teacher>
        {
            new Teacher { UserId = user!.Id, SchoolId = school!.Id, Subjects = enumerable, Grades = new List<Grade> { gradesEnumerable.First() } },
            new Teacher { UserId = user2!.Id, SchoolId = school.Id, Subjects = enumerable, Grades = new List<Grade> { gradesEnumerable.Last() } }
        };

        foreach (var teacher in teachers)
        {
            if (await unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == teacher.UserId) == null)
            {
                await unitOfWork.TeacherRepository.AddAsync(teacher);
            }
        }

        await unitOfWork.SaveAsync();
    }

    private static async Task SeedSubjectsAsync(IUnitOfWork unitOfWork)
    {
        var result = await unitOfWork.SubjectRepository.GetAllAsync();
        if (!result.Any())
        {
            var subjects = new List<Subject>
            {
                new Subject { Name = "Mathematics" },
                // Add more subjects
            };

            foreach (var subject in subjects)
            {
                if (await unitOfWork.SubjectRepository.FirstOrDefaultAsync(s => s.Name == subject.Name) == null)
                {
                    await unitOfWork.SubjectRepository.AddAsync(subject);
                }
            }
            await unitOfWork.SaveAsync();
        }
    }

    private static async Task SeedStudentsAsync(IUnitOfWork unitOfWork)
    {
        var user = await _userManager.FindByEmailAsync("user96@example.com");
        var user2 = await _userManager.FindByEmailAsync("user95@example.com");
        var grade = await unitOfWork.GradeRepository.FirstOrDefaultAsync(g => g.Name == "Grade 10");
        var school = await unitOfWork.SchoolRepository.FirstOrDefaultAsync(s => s.Name == "Eg Bertolt Brecht");
        await _userManager.AddToRoleAsync(user, Ts.Roles.Student);
        await _userManager.AddToRoleAsync(user2, Ts.Roles.Student);
        if (school is null)
        {
            await SeedSchoolsAsync(unitOfWork);
            school = await unitOfWork.SchoolRepository.FirstOrDefaultAsync(s => s.Name == "Eg Bertolt Brecht");
        }
        
        if (grade is null)
        {
            await SeedGradesAsync(unitOfWork);
            grade = await unitOfWork.GradeRepository.FirstOrDefaultAsync(g => g.Name == "Grade 10");
        }
        // Assuming User IDs and Grade IDs are already known or created
        var students = new List<Student>
        {
            new Student { UserId = user!.Id, GradeId = grade!.Id, SchoolId = school!.Id},
            new Student { UserId = user2!.Id, GradeId = grade.Id, SchoolId = school.Id}
        };

        foreach (var student in students)
        {
            if (await unitOfWork.StudentRepository.FirstOrDefaultAsync(s => s.UserId == student.UserId) == null)
            {
                await unitOfWork.StudentRepository.AddAsync(student);
            }
        }

        await unitOfWork.SaveAsync();
    }


    private static async Task SeedMarksAsync(IUnitOfWork unitOfWork)
    {
        //Teachers
        var user1 = await _userManager.FindByEmailAsync("user98@example.com");
        var user2 = await _userManager.FindByEmailAsync("user97@example.com");
        
        //Students
        var user3 = await _userManager.FindByEmailAsync("user96@example.com");
        var user4 = await _userManager.FindByEmailAsync("user95@example.com");
        
        var teacher1 = await unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == user1!.Id);
        var teacher2 = await unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == user2!.Id);
        var student1 = await unitOfWork.StudentRepository.FirstOrDefaultAsync(s => s.UserId == user3!.Id);
        var student2 = await unitOfWork.StudentRepository.FirstOrDefaultAsync(s => s.UserId == user4!.Id);
        var subject = await unitOfWork.SubjectRepository.FirstOrDefaultAsync(s => s.Id == 1);
        if (subject is null)
        {
            var teachers = new List<Teacher>() {teacher1!};
            var newSubject = new Subject
            {
                Name = "Higher Algebra",
                Teachers = teachers
            };
            await unitOfWork.SubjectRepository.AddAsync(newSubject);
            await unitOfWork.SaveAsync();
        }
        
        if (subject is null)
        {
            var teacherList = new List<Teacher> { teacher1! };
            subject = new Subject()
            {
                Name = "Software Technologies",
                Teachers = teacherList
            };
        }
        var marks = new List<Mark>
        {
            new Mark
            {
                Value = 3, StudentId = student1!.Id, TeacherId = teacher1!.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.ToUniversalTime(), Topic = "functions"
            },
            new Mark
            {
                Value = 4, StudentId = student1.Id, TeacherId = teacher1.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.AddDays(1).ToUniversalTime(), Topic = "functions"
            },
            new Mark
            {
                Value = 5, StudentId = student1.Id, TeacherId = teacher1.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.AddDays(2).ToUniversalTime(), Topic = "functions"
            },
            new Mark
            {
                Value = 6, StudentId = student1.Id, TeacherId = teacher1.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.AddDays(3).ToUniversalTime(), Topic = "functions"
            },
            new Mark
            {
                Value = 2, StudentId = student1.Id, TeacherId = teacher1.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.AddDays(4).ToUniversalTime(), Topic = "squareRoots"
            },
            new Mark
            {
                Value = 4, StudentId = student1.Id, TeacherId = teacher1.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.AddDays(5).ToUniversalTime(), Topic = "functions"
            },
            new Mark
            {
                Value = 3, StudentId = student1.Id, TeacherId = teacher1.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.AddDays(6).ToUniversalTime(), Topic = "functions"
            },
            new Mark
            {
                Value = 6, StudentId = student1.Id, TeacherId = teacher1.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.AddDays(7).ToUniversalTime(), Topic = "calculus"
            },
            new Mark
            {
                Value = 5, StudentId = student1.Id, TeacherId = teacher1.Id, SubjectId = subject.Id, GradedOn = DateTime.Now.AddDays(8).ToUniversalTime(), Topic = "functions"
            },
        };

        foreach (var mark in marks)
        {
            if (await unitOfWork.MarkRepository.FirstOrDefaultAsync(m =>
                    m.StudentId == mark.StudentId && m.SubjectId == mark.SubjectId &&
                    m.TeacherId == mark.TeacherId) is null)
            {
                await unitOfWork.MarkRepository.AddAsync(mark);
            }
        }

        await unitOfWork.SaveAsync();
    }

    private static async Task SeedGradesAsync(IUnitOfWork unitOfWork)
    {
        //Teachers
        var user1 = await _userManager.FindByEmailAsync("user98@example.com");
        var user2 = await _userManager.FindByEmailAsync("user97@example.com");
        
        var teacher1 = await unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == user1!.Id);
        var teacher2 = await unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == user2!.Id);
        var schools = await unitOfWork.SchoolRepository.GetAllAsync();
        var enumerable = schools.ToList();
        if (schools is null || enumerable.Any())
        {
            await SeedSchoolsAsync(unitOfWork);
            schools = await unitOfWork.SchoolRepository.GetAllAsync();
            enumerable = schools.ToList();
        }
        var grades = new List<Grade>
        {
            new() { Name = "Grade 10", ClassTeacherId = teacher1!.Id, Teachers = new List<Teacher> {teacher1}, SchoolId = enumerable.First().Id},
            new() { Name = "Grade 9", ClassTeacherId = teacher2!.Id, Teachers = new List<Teacher> {teacher2}, SchoolId = enumerable.First().Id},  
        };
        
        foreach (var grade in grades)
        {
            if (await unitOfWork.GradeRepository.FirstOrDefaultAsync(g => g.Name == grade.Name) == null)
            {
                await unitOfWork.GradeRepository.AddAsync(grade);
            }
        }
        await unitOfWork.SaveAsync();
    }

    private static async Task SeedAbsencesAsync(IUnitOfWork unitOfWork)
    {
        //Teachers
        var user1 = await _userManager.FindByEmailAsync("user98@example.com");
        var user2 = await _userManager.FindByEmailAsync("user97@example.com");

        //Students
        var user3 = await _userManager.FindByEmailAsync("user96@example.com");
        var user4 = await _userManager.FindByEmailAsync("user95@example.com");

        var teacher1 = await unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == user1!.Id);
        var teacher2 = await unitOfWork.TeacherRepository.FirstOrDefaultAsync(t => t.UserId == user2!.Id);

        var student1 = await unitOfWork.StudentRepository.FirstOrDefaultAsync(s => s.UserId == user3!.Id);
        var student2 = await unitOfWork.StudentRepository.FirstOrDefaultAsync(s => s.UserId == user3!.Id);
        var subject = await unitOfWork.SubjectRepository.FirstOrDefaultAsync(s => s.Name == "Mathematics");

        if (subject is null)
        {
            var teacherList = new List<Teacher> { teacher1! };
            var newSubject = new Subject()
            {
                Name = "Mathematics",
                Teachers = teacherList
            };
            await unitOfWork.SubjectRepository.AddAsync(newSubject);
            subject = await unitOfWork.SubjectRepository.FirstOrDefaultAsync(s => s.Name == newSubject.Name);
        }

        var absences = new List<Absence>
        {
            // Example data - replace with actual data
            new Absence
            {
                Value = 1.0m, TeacherId = teacher1!.Id, StudentId = student1!.Id, SubjectId = subject!.Id,
                Time = DateTime.Now.AddDays(-10).ToUniversalTime()
            },
            new Absence
            {
                Value = 0.5m, TeacherId = teacher1.Id, StudentId = student1.Id, SubjectId = subject.Id,
                Time = DateTime.Now.AddDays(-5).ToUniversalTime()   
            },
        };

        foreach (var absence in absences)
        {
            var existingAbsence = await unitOfWork.AbsenceRepository.FirstOrDefaultAsync(a =>
                a.StudentId == absence.StudentId && a.TeacherId == absence.TeacherId &&
                a.Time.Date == absence.Time.Date);

            if (existingAbsence == null)
            {
                await unitOfWork.AbsenceRepository.AddAsync(absence);
            }
        }
        await unitOfWork.SaveAsync();
    }

    private static async Task SeedSchoolsAsync(IUnitOfWork unitOfWork)
    {
        var schools = new List<School>
        {
            new School { Name = "Eg Bertolt Brecht" },
            new School { Name = "MG Pz" },
        };

        foreach (var school in schools)
        {
            if (await unitOfWork.SchoolRepository.FirstOrDefaultAsync(s => s.Name == school.Name) == null)
            {
                await unitOfWork.SchoolRepository.AddAsync(school);
            }
        }
        await unitOfWork.SaveAsync();
    }

}
