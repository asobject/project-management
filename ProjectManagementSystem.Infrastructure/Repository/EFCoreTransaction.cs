

using Microsoft.EntityFrameworkCore.Storage;
using Shared.Contracts.Repository;
using System.Threading;

namespace ProjectManagementSystem.Infrastructure.Repository;

public class EFCoreTransaction(IDbContextTransaction transaction) : ITransaction, IAsyncDisposable, IDisposable
{
    private bool _disposed;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        await transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        if (_disposed) return;
        transaction.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
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