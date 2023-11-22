using UniTrackBackend.Data;
using UniTrackBackend.Data.Seeding;
using UniTrackBackend.Infrastructure;
using UniTrackBackend.Interfaces;
using UniTrackBackend.Services;

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
                "http://localhost")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});
builder.Services.AddJwtToken(builder.Configuration);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApprovalService, ApprovalService>();
builder.Services.AddScoped<UnitOfWork>();
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

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DataSeeder.SeedData(app.Services).Wait();

app.Run();