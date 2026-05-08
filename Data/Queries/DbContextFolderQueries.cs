namespace Quizly.Data.Queries;

using Microsoft.EntityFrameworkCore;
using Quizly.Domain;

public class DbContextFolderQueries(QuizlyDbContext dbContext) : IFolderQueries
{
    public async Task<IReadOnlyList<IFolderQueries.FolderResponse>> GetAllFoldersAsync(string userId)
        => await dbContext.Folders
            .Where(f => f.UserId == userId)
            .Select(f => new IFolderQueries.FolderResponse(f.Id.Value, f.Name))
            .ToListAsync();

    public async Task<IFolderQueries.FolderResponse?> GetByIdAsync(FolderId id, string userId)
        => await dbContext.Folders
            .Where(f => f.Id == id && f.UserId == userId)
            .Select(f => new IFolderQueries.FolderResponse(f.Id.Value, f.Name))
            .FirstOrDefaultAsync();
}
