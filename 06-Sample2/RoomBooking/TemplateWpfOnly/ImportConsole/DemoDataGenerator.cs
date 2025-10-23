using Bogus;

using Core.Entities;

namespace ImportConsole;

public class DemoDataGenerator
{
    private const int NUMBER_OF_ROOMS     = 20;
    private const int NUMBER_OF_CUSTOMERS = 40;

    public static DemoData Generate()
    {
        var result = new DemoData();

        var customers = new Faker<Customer>("de_AT")
            .RuleFor(u => u.FirstName,        f => f.Name.FirstName())
            .RuleFor(u => u.LastName,         (f, u) => f.Name.LastName())
            .RuleFor(u => u.EmailAddress,     (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.CreditCardNumber, (f, u) => f.Finance.CreditCardNumber());
        result.Customers = customers.Generate(NUMBER_OF_CUSTOMERS);


        var rooms = new Faker<Room>()
            .RuleFor(r => r.RoomNumber, f => $"R-{f.UniqueIndex}")
            .RuleFor(r => r.RoomType,   f => f.PickRandom<RoomType>());
        result.Rooms = rooms.Generate(NUMBER_OF_ROOMS);

        result.Bookings = new();
        var random = new Random();

        foreach (var room in result.Rooms)
        {
            var pastMonth      = 24;
            var nrPastBookings = random.Next(3, 11);
            var bookingFaker = new Faker<Booking>()
                .RuleFor(r => r.From,     f => f.Date.Between(DateTime.Now.AddMonths(-pastMonth--), DateTime.Now.AddMonths(-pastMonth--)))
                .RuleFor(r => r.To,       (f, r) => f.Date.Between(r.From.AddDays(1),               r.From.AddDays(14)))
                .RuleFor(r => r.Customer, f => f.PickRandom(result.Customers))
                .RuleFor(r => r.Room,     room);
            var bookings = bookingFaker.Generate(nrPastBookings);
            result.Bookings.AddRange(bookings);
        }

        var currentBookings = new Faker<Booking>()
            .RuleFor(r => r.From,     f => f.Date.Between(DateTime.Now, DateTime.Now.AddDays(-5)))
            .RuleFor(r => r.Customer, f => f.PickRandom(result.Customers))
            .RuleFor(r => r.Room,     f => f.PickRandom(result.Rooms));
        var bookingsWithDuplicates = currentBookings.Generate(30);
        var currentBookingsGenerated = bookingsWithDuplicates
            .GroupBy(b => b.Room)
            .Select(grp => grp.First())
            .ToList();
        result.Bookings.AddRange(currentBookingsGenerated);

        return result;
    }
}