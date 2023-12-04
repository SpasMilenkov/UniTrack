using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Models.TypeSafe;

namespace UniTrackBackend.Data.Seeding;

public static class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        
        await SeedGradesAsync(unitOfWork);
        await SeedRolesAsync(roleManager);
        await SeedUsersAsync(userManager);
        await SeedTeachersAsync(unitOfWork);
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

    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        var users = new List<User>
        {
            new User { UserName = "user1", FirstName = "John", LastName = "Doe", Email = "user98@example.com", AvatarUrl = "example"},
            new User { UserName = "user2", FirstName = "John", LastName = "Doe", Email = "user97@example.com", AvatarUrl = "example"},
            new User { UserName = "user3", FirstName = "John", LastName = "Doe", Email = "user96@example.com", AvatarUrl = "example"},
            new User { UserName = "user4", FirstName = "John", LastName = "Doe", Email = "user95@example.com", AvatarUrl = "example"},
            new User { UserName = "user5", FirstName = "John", LastName = "Doe", Email = "user94@example.com", AvatarUrl = "example"},
            // Add more users
        };

        foreach (var user in users)
        {
            if (await userManager.FindByEmailAsync(user.Email!) == null)
            {
                await userManager.CreateAsync(user, "String123!");
            }
        }
    }

    private static async Task SeedTeachersAsync(UnitOfWork unitOfWork)
    {
        var result = await unitOfWork.TeacherRepository.GetAllAsync();
        if (!result.Any())
        {
            // Assuming User IDs are already known or created
            var teachers = new List<Teacher>
            {
                new Teacher { UserId = "1e88293d-81a2-42ca-80c5-88266f053e2b" },
                new Teacher { UserId = "6010351f-d12b-4235-83fe-cfb7ddce26b8"},
                new Teacher {UserId = "d417aa45-e4e2-4096-ba6c-e96791feffc0" },
                // Add more teachers
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
    }

    private static async Task SeedSubjectsAsync(UnitOfWork unitOfWork)
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

    private static async Task SeedStudentsAsync(UnitOfWork unitOfWork)
    {
        // Assuming User IDs and Grade IDs are already known or created
        var students = new List<Student>
        {
            new Student { UserId = "1e88293d-81a2-42ca-80c5-88266f053e2b", GradeId = 37},
            new Student { UserId = "ae845d0a-af39-4db3-83a8-9e1ca2a9fc80", GradeId = 37}
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


    private static async Task SeedMarksAsync(UnitOfWork unitOfWork)
    {
        var marks = new List<Mark>
        {
            new Mark
            {
                Value = 3, StudentId = 12, TeacherId = 2, SubjectId = 1, GradedOn = DateTime.Now.ToUniversalTime()
            },
            new Mark
            {
                Value = 4, StudentId = 12, TeacherId = 2, SubjectId = 1,
                GradedOn = DateTime.Now.AddMinutes(1).ToUniversalTime()
            },
            new Mark
            {
                Value = 2, StudentId = 12, TeacherId = 2, SubjectId = 1,
                GradedOn = DateTime.Now.AddMinutes(2).ToUniversalTime()
            },
            new Mark
            {
                Value = 6, StudentId = 12, TeacherId = 2, SubjectId = 1,
                GradedOn = DateTime.Now.AddMinutes(3).ToUniversalTime()
            },
            new Mark
            {
                Value = 6, StudentId = 12, TeacherId = 2, SubjectId = 1,
                GradedOn = DateTime.Now.AddMinutes(4).ToUniversalTime()
            },
            new Mark
            {
                Value = 5, StudentId = 12, TeacherId = 2, SubjectId = 1,
                GradedOn = DateTime.Now.AddMinutes(5).ToUniversalTime()
            },
            new Mark
            {
                Value = 4, StudentId = 12, TeacherId = 2, SubjectId = 1,
                GradedOn = DateTime.Now.AddMinutes(6).ToUniversalTime()
            },
            new Mark
            {
                Value = 3, StudentId = 12, TeacherId = 2, SubjectId = 1,
                GradedOn = DateTime.Now.AddMinutes(7).ToUniversalTime()
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

    private static async Task SeedGradesAsync(UnitOfWork unitOfWork)
    {
        var grades = new List<Grade>
        {
            new Grade { Name = "Grade 10", ClassTeacherId = 2 },
            // Add more grades
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
    
    private static async Task SeedElectiveSubjectsAsync(UnitOfWork unitOfWork)
    {
        var result = await unitOfWork.ElectiveSubjectRepository.GetAllAsync();
        if (!result.Any())
        {
            var electiveSubjects = new List<ElectiveSubject>
            {
                new ElectiveSubject { Name = "Advanced Mathematics"},
                new ElectiveSubject { Name = "Environmental Science" },
            };

            foreach (var electiveSubject in electiveSubjects)
            {
                if (await unitOfWork.ElectiveSubjectRepository.FirstOrDefaultAsync(es => es.Name == electiveSubject.Name) == null)
                {
                    await unitOfWork.ElectiveSubjectRepository.AddAsync(electiveSubject);
                }
            }
        }
    }
    private static async Task SeedAbsencesAsync(UnitOfWork unitOfWork)
    {
            var absences = new List<Absence>
            {
                // Example data - replace with actual data
                new Absence { Value = 1.0m, TeacherId = 2, StudentId = 12, SubjectId  = 1, Time = DateTime.Now.AddDays(-10).ToUniversalTime() },
                new Absence { Value = 0.5m, TeacherId = 2, StudentId = 12, SubjectId  = 1, Time = DateTime.Now.AddDays(-5).ToUniversalTime() },
            };

            foreach (var absence in absences)
            {
                var existingAbsence = await unitOfWork.AbsenceRepository.FirstOrDefaultAsync(a =>
                    a.StudentId == absence.StudentId && a.TeacherId == absence.TeacherId && a.Time.Date == absence.Time.Date);

                if (existingAbsence == null)
                {
                    await unitOfWork.AbsenceRepository.AddAsync(absence);
                }
            }

            await unitOfWork.SaveAsync();
    }

}
