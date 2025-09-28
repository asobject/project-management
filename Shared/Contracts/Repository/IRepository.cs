
using CSharpFunctionalExtensions;
using Shared.DTOs;
using Shared.Errors;
using System.Linq.Expressions;

namespace Shared.Contracts.Repository;

public interface IRepository<TEntity, TId>
     where TEntity : Entity<TId>
    where TId : IComparable<TId>, IEquatable<TId>
{
    Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken = default);
    TId Save(TEntity entity);
    TId Delete(TEntity entity);
    TId Update(TEntity entity);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    Task<Result<PagedResultDTO<TEntity>, Error>> GetPagedAsync(
       int pageNumber,
       int pageSize,
       Expression<Func<TEntity, bool>>? predicate = null,
       CancellationToken cancellationToken = default,
       params Expression<Func<TEntity, object>>[] includes);
}
