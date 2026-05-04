using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quizly.Data;
using Quizly.Domain;

namespace Quizly.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddQuizlyDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? "Data Source=quizly.db";

        services.AddDbContext<QuizlyDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }

    public static IServiceCollection AddQuizlyIdentity(this IServiceCollection services)
    {
        services.AddIdentityApiEndpoints<QuizlyUser>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false; // Set to true if you want email confirmation
        })
        .AddEntityFrameworkStores<QuizlyDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
