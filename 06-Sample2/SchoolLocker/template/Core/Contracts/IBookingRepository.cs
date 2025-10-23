using Core.Entities;

namespace Core.Contracts;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<IEnumerable<Booking>> GetFilteredBookingsAsync(string?   lockerNumber, DateTime? dateFrom, DateTime? dateTo);
    Task<Booking>              GetOverlappingBookingAsync(Booking booking);
    Task<Booking[]>            GetOverlappingBookingsAsync(int    lockerNumber, DateTime @from, DateTime? to);
}