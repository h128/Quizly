namespace Quizly.Data;

public interface IUnitOfWork
{
    IRepository<TAggregate, TKey> GetRepository<TAggregate, TKey>() where TAggregate : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
