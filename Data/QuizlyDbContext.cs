using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quizly.Domain;

namespace Quizly.Data;

public partial class QuizlyDbContext(DbContextOptions<QuizlyDbContext> options) : IdentityDbContext<QuizlyUser>(options), IUnitOfWork
{
    public DbSet<Folder> Folders => Set<Folder>();

    public IRepository<TAggregate, TKey> GetRepository<TAggregate, TKey>() where TAggregate : class
    => (IRepository<TAggregate, TKey>)this;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply all configurations from the current assembly
        builder.ApplyConfigurationsFromAssembly(typeof(QuizlyDbContext).Assembly);
    }
}
