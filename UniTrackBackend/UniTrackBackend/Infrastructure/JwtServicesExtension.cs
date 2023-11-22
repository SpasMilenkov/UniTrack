using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace UniTrackBackend.Infrastructure;

public static class JwtServicesExtension
{
    public static void AddJwtToken(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("AccessToken"))
                        {
                            context.Token = context.Request.Cookies["AccessToken"];
                        }

                        return Task.CompletedTask;
                    }
                };
            });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "UniTrack API",
                Description = "The ASP.NET Core Web API behind the UniTrack School Management System and analysis tool.",
                Contact = new OpenApiContact
                {
                    Name = "Spas Milenkov",
                    Email = "dummy@example.com",
                    Url = new Uri("https://github.com/SpasMilenkov")
                },
                Version = "v1"
            });
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            
            options.IncludeXmlComments(xmlPath);
            
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard authorization header using bearer token",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            
        });
    }
}