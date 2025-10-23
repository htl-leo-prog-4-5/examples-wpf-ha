using Core.Contracts;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext? _dbContext;

    public UnitOfWork() : this(new ApplicationDbContext())
    {
    }

    public UnitOfWork(ApplicationDbContext dBContext)
    {
        _dbContext = dBContext;
        Pupils     = new PupilRepository(_dbContext);
        Lockers    = new LockerRepository(_dbContext);
        Bookings   = new BookingRepository(_dbContext);
    }

    public IPupilRepository   Pupils   { get; }
    public ILockerRepository  Lockers  { get; }
    public IBookingRepository Bookings { get; }


    public async Task<int> SaveChangesAsync()
    {
        var entities = _dbContext!.ChangeTracker.Entries()
            .Where(entity => entity.State == EntityState.Added
                             || entity.State == EntityState.Modified)
            .Select(e => e.Entity)
            .ToArray(); // Geänderte Entities ermitteln

        // Allfällige Validierungen der geänderten Entities durchführen
        foreach (var entity in entities)
        {
            await ValidateEntityAsync(entity);
        }

        return await _dbContext.SaveChangesAsync();
    }

    private async Task ValidateEntityAsync(object entity)
    {
        Validator.ValidateObject(entity, new ValidationContext(entity), true);
        await using var uow = new UnitOfWork();
    }

    public async Task DeleteDatabaseAsync()  => await _dbContext!.Database.EnsureDeletedAsync();
    public async Task MigrateDatabaseAsync() => await _dbContext!.Database.MigrateAsync();
    public async Task CreateDatabaseAsync()  => await _dbContext!.Database.EnsureCreatedAsync();

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (_dbContext != null)
        {
            if (disposing)
            {
                await _dbContext.DisposeAsync();
            }
        }

        _dbContext = null;
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}