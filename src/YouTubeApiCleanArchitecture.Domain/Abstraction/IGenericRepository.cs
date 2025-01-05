using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Domain.Abstraction;
public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll();

    Task<List<TResponse>> GetAllAsync<TResponse>(
       IMapper mapper,
       bool enableTracking = true,
       Expression<Func<TEntity, bool>>[]? predicates = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? include = null,
       CancellationToken cancellationToken = default)
       where TResponse : IResult;

    Task<TEntity?> GetAsync(
        bool enableTracking = false,
        Expression<Func<TEntity, bool>>[]? predicates = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes = null,
        CancellationToken cancellationToken = default);

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
