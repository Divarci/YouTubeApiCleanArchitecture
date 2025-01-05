using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;

namespace YouTubeApiCleanArchitecture.Domain.Abstraction;
public interface IUnitOfWork
{
    Task CommitAsync(
     CancellationToken cancellationToken = default,
     bool checkForConcurrency = false);

    IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseEntity;
}
