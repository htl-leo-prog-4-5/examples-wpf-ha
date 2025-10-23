using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;


namespace Persistence;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Booking>> GetFilteredBookingsAsync(string? lockerNumber, DateTime? dateFrom, DateTime? dateTo)
    {
        IQueryable<Booking> query = _dbContext.Bookings.Include(b => b.Locker);
        if (lockerNumber != null)
        {
            query = query.Where(b => b.Locker!.Number == int.Parse(lockerNumber));
        }

        if (dateFrom != null)
        {
            query = query.Where(b => b.From >= dateFrom || b.To == null || (b.To != null && b.To >= dateFrom));
        }

        if (dateTo != null)
        {
            query = query.Where(b => b.To <= dateTo || b.To == null || b.From <= dateTo);
        }

        return await query.ToListAsync();
    }

    public async Task<Booking> GetOverlappingBookingAsync(Booking booking)
    {
        var end = booking.To ?? DateTime.MaxValue;

        return (await _dbContext.Bookings.FirstOrDefaultAsync(b =>
            b.Id != booking.Id && booking.LockerId == b.LockerId && (
                (b.From >= booking.From && b.From <= end) //True if b.From is in range of booking
                || (b.To >= booking.From && b.To <= end)  //True if b.To is in range of booking
                || (b.From < booking.From && b.To > end)  //True if booking is completely inside b
            )))!;
    }

    public async Task<Booking[]> GetOverlappingBookingsAsync(int lockerNumber, DateTime @from, DateTime? to)
    {
        if (to == null)
        {
            to = DateTime.MaxValue;
        }

        var bookings = await _dbContext
            .Bookings
            .Where(b => b.LockerId == lockerNumber &&
                        (b.From >= @from && b.From <= to ||
                         b.To >= @from && (b.To != null && b.To <= to))
            ).ToArrayAsync();
        return bookings;
    }
}