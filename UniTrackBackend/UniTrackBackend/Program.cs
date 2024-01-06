using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Seeding;
using UniTrackBackend.Infrastructure;
using UniTrackBackend.Middlewares;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;
using UniTrackBackend.Services.SubjectService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin",
        options => options.WithOrigins(
                "https://localhost:5500",
                "http://127.0.0.1:5500",
                "http://localhost:5173/",
                "http://localhost:5173",
                "http://localhost:4200",
                "http://localhost")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});
builder.Services.AddJwtToken(builder.Configuration);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApprovalService, ApprovalService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IAnalysisService, AnalysisService>();
builder.Services.AddScoped<IMarkService, MarkService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IAbsenceService, AbsenceService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AllowOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// DataSeeder.SeedData(app.Services).Wait();

app.Run();