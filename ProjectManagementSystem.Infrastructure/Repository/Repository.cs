

using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Infrastructure.Data;
using Shared.Contracts.Repository;
using Shared.DTOs;
using Shared.Errors;
using System.Linq.Expressions;

namespace ProjectManagementSystem.Infrastructure.Repository;

public class Repository<TEntity, TId>(ApplicationDbContext context) : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : IComparable<TId>, IEquatable<TId>
{
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => (await _dbSet.AddAsync(entity, cancellationToken)).Entity.Id;
    public TId Update(TEntity entity)
        => _dbSet.Update(entity).Entity.Id;
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }
    public TId Delete(TEntity entity)
        => _dbSet.Remove(entity).Entity.Id;
    public TId Save(TEntity entity)
        => _dbSet.Attach(entity).Entity.Id;
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
   => await _dbSet.AnyAsync(predicate);

    public async Task<Result<PagedResultDTO<TEntity>, Error>> GetPagedAsync(
         int pageNumber, int pageSize,
         Expression<Func<TEntity, bool>>? predicate = null,
         CancellationToken cancellationToken = default,
         params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        int totalRecords = await query.CountAsync(cancellationToken);

        var data = await query
            .OrderBy(e => e.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResultDTO<TEntity>(data, totalRecords);
    }

    public async Task<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken = default ) =>
       await _dbSet.FindAsync(keyValues, cancellationToken: cancellationToken);
}