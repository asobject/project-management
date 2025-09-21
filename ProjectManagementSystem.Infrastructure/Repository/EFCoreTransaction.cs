

using Microsoft.EntityFrameworkCore.Storage;
using Shared.Contracts.Repository;

namespace ProjectManagementSystem.Infrastructure.Repository;

public class EFCoreTransaction(IDbContextTransaction transaction) : ITransaction, IAsyncDisposable, IDisposable
{
    private bool _disposed;

    public async Task CommitAsync()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        await transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        await transaction.RollbackAsync();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            transaction.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            await transaction.DisposeAsync();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}