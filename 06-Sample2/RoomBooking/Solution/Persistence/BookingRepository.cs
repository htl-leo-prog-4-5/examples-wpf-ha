using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using Base.Persistence;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Booking>> GetBookingsForCustomer(int customerId)
    {
        return await _dbContext.Bookings
            .Include(b => b.Customer)
            .Include(b => b.Room)
            .Where(b => b.CustomerId == customerId)
            .OrderByDescending(b => b.From)
            .ToListAsync();
    }
}