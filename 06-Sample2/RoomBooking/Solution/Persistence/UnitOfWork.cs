using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public UnitOfWork() : this(new ApplicationDbContext())
    {
    }

    public UnitOfWork(ApplicationDbContext dBContext) : base(dBContext)
    {
        Rooms     = new RoomRepository(dBContext);
        Customers = new CustomerRepository(dBContext);
        Bookings  = new BookingRepository(dBContext);
    }

    public ICustomerRepository Customers { get; }
    public IRoomRepository     Rooms     { get; }
    public IBookingRepository  Bookings  { get; }
}