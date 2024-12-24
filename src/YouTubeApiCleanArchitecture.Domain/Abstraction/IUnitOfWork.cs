namespace YouTubeApiCleanArchitecture.Domain.Abstraction;
public interface IUnitOfWork
{
    Task<string> CommitAsync(
     CancellationToken cancellationToken = default,
     bool checkForConcurrency = false);

    IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseEntity;
}
