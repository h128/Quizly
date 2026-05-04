using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quizly.Domain;

namespace Quizly.Data;

public class QuizlyDbContext : IdentityDbContext<QuizlyUser>
{
    public QuizlyDbContext(DbContextOptions<QuizlyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply all configurations from the current assembly
        builder.ApplyConfigurationsFromAssembly(typeof(QuizlyDbContext).Assembly);
    }
}
