namespace Quizly.Domain;

public sealed record FolderId(Guid Value)
{
    public static FolderId New() => new(Guid.NewGuid());
}

public class Folder
{
    private Folder() { } // for EF Core

    public static Folder Create(FolderId id, string name, string userId)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        return new Folder { Id = id, Name = name, UserId = userId };
    }

    public void Rename(string newName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newName);
        Name = newName;
    }

    public FolderId Id { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string UserId { get; private set; } = null!;
    public QuizlyUser User { get; private set; } = null!;
}

