using AuthenticationBase.Persistence;

namespace AuthenticationBase.Contracts.Persistence;

public interface IBaseUnitOfWork : IDisposable, IAsyncDisposable
{
    BaseApplicationDbContext BaseApplicationDbContext { get; init; }

    ISessionRepository         Sessions         { get; }
    IApplicationUserRepository ApplicationUsers { get; }

    Task<int> SaveChangesAsync();
    Task      DeleteDatabaseAsync();
    Task      MigrateDatabaseAsync();
    Task      CreateDatabaseAsync();
}