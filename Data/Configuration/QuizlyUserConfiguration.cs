using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizly.Domain;

namespace Quizly.Data.Configuration;

public class QuizlyUserConfiguration : IEntityTypeConfiguration<QuizlyUser>
{
    public void Configure(EntityTypeBuilder<QuizlyUser> builder)
    {
        builder.Property(e => e.FirstName).HasMaxLength(100);
        builder.Property(e => e.LastName).HasMaxLength(100);
        builder.HasIndex(e => e.Email).IsUnique();
    }
}
