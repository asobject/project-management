
using CSharpFunctionalExtensions;
using Shared.DTOs;
using Shared.Errors;
using System.Linq.Expressions;

namespace Shared.Contracts.Repository;

public interface IRepository<TEntity, TId>
     where TEntity : Entity<TId>
    where TId : IComparable<TId>, IEquatable<TId>
{
    Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Guid Save(TEntity entity, CancellationToken cancellationToken = default);
    Guid Delete(TEntity entity, CancellationToken cancellationToken = default);

    Task<Result<PageResultDTO<TEntity>, Error>> GetPagedAsync(
       int pageNumber,
       int pageSize,
       Expression<Func<TEntity, bool>>? predicate = null,
       params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includeFuncs);
}
