namespace Quizly.Data;

public interface IRepository<TAggregate, TKey>
{
    Task<TAggregate?> TryFindAsync(TKey key);
    void Add(TAggregate aggregate);
    void Delete(TAggregate aggregate);
    async Task DeleteAsync(TKey key)
    {
        var aggregate = await TryFindAsync(key) ?? throw new InvalidOperationException("Aggregate not found");
        Delete(aggregate);
    }
}
