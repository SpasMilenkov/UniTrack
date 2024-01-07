using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Database;

public class UniTrackDbContext : IdentityDbContext<User>
{
    public UniTrackDbContext(DbContextOptions<UniTrackDbContext> options)
        : base(options)
    {
    }

    public DbSet<Absence> Absences { get; set; } = null!;
    public DbSet<Grade> Grades { get; set; } = null!;
    public DbSet<Mark> Marks { get; set; } = null!;
    public DbSet<Parent> Parents { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<School> Schools { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<GradeSubjectTeacher> GradeSubjectTeachers { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Parent>()
            .HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<Parent>(p => p.UserId);
        
        modelBuilder.Entity<Student>()
            .HasOne(s => s.User)
            .WithOne()
            .HasForeignKey<Student>(s => s.UserId);

        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.User)
            .WithOne()
            .HasForeignKey<Teacher>(t => t.UserId);
        
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.ClassTeacher)
            .WithOne() // If Teacher doesn't have a corresponding navigation property for Grade
            .HasForeignKey<Grade>(g => g.ClassTeacherId);
        
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Grades)
        .WithMany(g => g.Teachers);
        
        // GradeSubjectTeacher configuration
        modelBuilder.Entity<GradeSubjectTeacher>()
            .HasKey(gst => new { gst.GradeId, gst.TeacherId, gst.SubjectId });

        modelBuilder.Entity<GradeSubjectTeacher>()
            .HasOne(gst => gst.Grade)
            .WithMany(g => g.GradeSubjectTeachers)
            .HasForeignKey(gst => gst.GradeId);
        
    }
}
