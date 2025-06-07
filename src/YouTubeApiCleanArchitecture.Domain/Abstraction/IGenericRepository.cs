using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;

namespace YouTubeApiCleanArchitecture.Domain.Abstraction;
public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll();

    TResult Query<TResult>(Func<IQueryable<TEntity>, TResult> query);

    Task<TResult> QueryAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> query);

    Task<TEntity?> GetByIdAsync(
      Guid id,
      CancellationToken cancellationToken = default);

    Task<TEntity> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task CreateRangeAsync(
        IEnumerable<TEntity> entityCollection,
        CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entityCollection);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entityCollection);
}
