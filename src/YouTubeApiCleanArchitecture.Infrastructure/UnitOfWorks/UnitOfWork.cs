using Microsoft.EntityFrameworkCore;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;
using YouTubeApiCleanArchitecture.Domain.Exceptions;
using YouTubeApiCleanArchitecture.Infrastructure.Repositories;

namespace YouTubeApiCleanArchitecture.Infrastructure.UnitOfWorks;
public class UnitOfWork(
    AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public async Task CommitAsync(
        CancellationToken cancellationToken = default, 
        bool checkForConcurrency = false)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException) when (checkForConcurrency)
        {
            throw new ConcurrencyException(
                ["A concurrency conflict occurred while saving changes"]);
        }
    }

    public IGenericRepository<TEntity> Repository<TEntity>() 
        where TEntity : BaseEntity
        => new GenericRepository<TEntity>(_context);    
}
