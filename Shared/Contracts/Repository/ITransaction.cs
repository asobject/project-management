namespace Shared.Contracts.Repository;

public interface ITransaction : IAsyncDisposable,IDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}