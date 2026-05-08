using Quizly.Domain;

namespace Quizly.Data;

public interface IFolderQueries
{
    public record FolderResponse(Guid Id, string Name);

    Task<IReadOnlyList<FolderResponse>> GetAllFoldersAsync(string userId);
    Task<FolderResponse?> GetByIdAsync(FolderId id, string userId);
}
