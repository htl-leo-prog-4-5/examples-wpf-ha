using Microsoft.EntityFrameworkCore;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

namespace Persistence;

internal class LockerRepository : GenericRepository<Locker>, ILockerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LockerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Locker?> GetByNumberAsync(int number)
    {
        return await _dbContext.Lockers
            .Include(l => l.Bookings)
            .ThenInclude(b => b.Pupil)
            .SingleOrDefaultAsync(l => l.Number == number);
    }

    public async Task<bool> HasDuplicateAsync(Locker locker) => await _dbContext.Lockers.AnyAsync(l => l.Id != locker.Id && l.Number == locker.Number);

    public async Task<IEnumerable<LockerBooking>> GetLockersOverviewAsync()
    {
        var bookings = await _dbContext.Bookings
            .Include(b => b.Locker)
            .OrderByDescending(b => b.From)
            .ToListAsync();
        var groupedBookings = bookings
            .GroupBy(b => b.Locker)
            .Select(group => new LockerBooking
            {
                Locker = group.Key!,
                Count  = group.Count(),
                From   = group.First().From,
                To     = group.First().To
            })
            .OrderBy(lo => lo.Locker.Number)
            .ToList();
        return groupedBookings;
    }

    public async Task<SchoolLockerDto[]> GetLockersWithStateAsync()
    {
        var lockers = await _dbContext
            .Lockers
            .Select(l => new SchoolLockerDto
            {
                Number        = l.Number,
                CountBookings = l.Bookings.Count,
                IsTodayFree = !l.Bookings.Any(b => (b.From <= DateTime.Today && b.To == null ||
                                                    b.From <= DateTime.Today && b.To >= DateTime.Today))
            })
            .OrderBy(l => l.Number)
            .ToArrayAsync();
        return lockers;
    }
}