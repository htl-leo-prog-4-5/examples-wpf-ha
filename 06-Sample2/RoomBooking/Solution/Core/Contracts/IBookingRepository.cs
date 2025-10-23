using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<List<Booking>> GetBookingsForCustomer(int id);
}