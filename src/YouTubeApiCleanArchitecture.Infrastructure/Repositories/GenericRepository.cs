using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;

namespace YouTubeApiCleanArchitecture.Infrastructure.Repositories;
public class GenericRepository<TEntity>(
    AppDbContext context) : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly AppDbContext _context = context;

    public IQueryable<TEntity> GetAll() 
        => _context
            .Set<TEntity>()
            .AsNoTracking()
            .AsQueryable();

    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) 
        => await _context
            .Set<TEntity>()
            .FindAsync([id, cancellationToken], cancellationToken);

    public async Task<TEntity> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _context
            .Set<TEntity>()
            .AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task CreateRangeAsync(
        IEnumerable<TEntity> entityCollection,
        CancellationToken cancellationToken = default)
        => await _context
            .Set<TEntity>()
            .AddRangeAsync(entityCollection, cancellationToken);

    public TEntity Update(TEntity entity)
    {
        _context.Set<TEntity>()
            .Update(entity);

        return entity;
    }

    public void UpdateRange(IEnumerable<TEntity> entityCollection) 
        => _context
            .Set<TEntity>()
            .UpdateRange(entityCollection);

    public void Delete(TEntity entity)
        => _context
            .Set<TEntity>()
            .Remove(entity);

    public void DeleteRange(IEnumerable<TEntity> entityCollection) 
        => _context
            .Set<TEntity>()
            .RemoveRange(entityCollection);

    public async Task<List<TResponse>> GetAllAsync<TResponse>(
       IMapper mapper,
       bool enableTracking = true,
       Expression<Func<TEntity, bool>>[]? predicates = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes = null,
       CancellationToken cancellationToken = default)
       where TResponse : IResult
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (!enableTracking)
            query = query.AsNoTracking();

        if (includes is not null)
            foreach (var include in includes)
                query = include(query);

        if (predicates is not null)
            foreach (var predicate in predicates)
                query = query.Where(predicate);

        if (orderBy is not null)
            query = orderBy(query);

        var response = query.ProjectTo<TResponse>(mapper.ConfigurationProvider);

        return await response.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(
        bool enableTracking = false,
        Expression<Func<TEntity, bool>>[]? predicates = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (!enableTracking)
            query = query.AsNoTracking();

        if (includes is not null)
            foreach (var include in includes)
                query = include(query);

        if (predicates is not null)
            foreach (var predicate in predicates)
                query = query.Where(predicate);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
