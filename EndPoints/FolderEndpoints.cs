using Microsoft.AspNetCore.Mvc;
using Quizly.Data;
using Quizly.Domain;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Quizly.EndPoints;

public static class FolderEndpoints
{
    public sealed record CreateFolderRequest([Required] string Name);
    public sealed record UpdateFolderRequest([Required] string Name);

    public static IEndpointRouteBuilder MapFolderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/folders").WithTags("Folders").RequireAuthorization();

        group.MapGet("/", static async (ClaimsPrincipal user, [FromServices] IFolderQueries queries) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var folders = await queries.GetAllFoldersAsync(userId);
            return Results.Ok(folders);
        })
        .WithName("GetFolders");

        group.MapGet("/{id:guid}", static async (Guid id, ClaimsPrincipal user,
            [FromServices] IFolderQueries queries) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var folder = await queries.GetByIdAsync(new FolderId(id), userId);
            return folder is null ? Results.NotFound() : Results.Ok(folder);
        })
        .WithName("GetFolderById");

        group.MapPost("/", static async ([FromBody] CreateFolderRequest request,
            ClaimsPrincipal user, [FromServices] IUnitOfWork unitOfWork,
            CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var repository = unitOfWork.GetRepository<Folder, FolderId>();
            var folder = Folder.Create(FolderId.New(), request.Name, userId);

            repository.Add(folder);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new IFolderQueries.FolderResponse(folder.Id.Value, folder.Name);
            return Results.Created($"/api/folders/{folder.Id.Value}", response);
        })
        .WithName("CreateFolder");

        group.MapPut("/{id:guid}", static async (Guid id, [FromBody] UpdateFolderRequest request,
            ClaimsPrincipal user, [FromServices] IUnitOfWork unitOfWork,
            CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var repository = unitOfWork.GetRepository<Folder, FolderId>();
            var folder = await repository.TryFindAsync(new FolderId(id));

            if (folder is null || folder.UserId != userId)
                return Results.NotFound();

            folder.Rename(request.Name);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Results.Ok(new IFolderQueries.FolderResponse(folder.Id.Value, folder.Name));
        })
        .WithName("UpdateFolder");

        return app;
    }
}
