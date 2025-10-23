namespace Core.Contracts;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    IPupilRepository   Pupils   { get; }
    IBookingRepository Bookings { get; }
    ILockerRepository  Lockers  { get; }

    Task<int> SaveChangesAsync();
    Task      DeleteDatabaseAsync();
    Task      MigrateDatabaseAsync();
    Task      CreateDatabaseAsync();
}