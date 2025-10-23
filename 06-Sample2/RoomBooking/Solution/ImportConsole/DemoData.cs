using Core.Entities;

namespace ImportConsole;

public class DemoData
{
    public List<Customer> Customers { get; set; } = new();
    public List<Room>     Rooms     { get; set; } = new();

    public List<Booking> Bookings { get; set; } = new();
}