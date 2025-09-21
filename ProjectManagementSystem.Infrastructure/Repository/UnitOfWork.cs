
using ProjectManagementSystem.Infrastructure.Data;
using Shared.Contracts.Repository;

namespace ProjectManagementSystem.Infrastructure.Repository;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var dbTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
        return new EFCoreTransaction(dbTransaction);
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);
}
