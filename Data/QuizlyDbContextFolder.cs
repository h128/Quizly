using Quizly.Domain;

namespace Quizly.Data;

public partial class QuizlyDbContext : IRepository<Folder, FolderId>
{
    public async Task<Folder?> TryFindAsync(FolderId key) => await Folders.FindAsync(key.Value);
    public void Add(Folder aggregate) => Folders.Add(aggregate);

    public void Delete(Folder aggregate) => Folders.Remove(aggregate);
}
