namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    IBookingRepository  Bookings  { get; }
    ICustomerRepository Customers { get; }
    IRoomRepository     Rooms     { get; }
}