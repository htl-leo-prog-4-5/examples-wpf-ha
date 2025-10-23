using Persistence;

namespace ImportConsole;

class Program
{
    static async Task Main(string[] args)
    {
        await using var uow = new UnitOfWork();
        Console.WriteLine("Datenbank migrieren");
        await uow.DeleteDatabaseAsync();
        //await uow.CreateDatabaseAsync();
        await uow.MigrateDatabaseAsync();

        Console.WriteLine("Daten generieren");

        var data = DemoDataGenerator.Generate();

        Console.WriteLine("Daten speichern");
        await uow.Customers.AddRangeAsync(data.Customers);
        await uow.Rooms.AddRangeAsync(data.Rooms);
        await uow.Bookings.AddRangeAsync(data.Bookings);

        await uow.SaveChangesAsync();

        var customersCount = await uow.Customers.CountAsync();
        var roomsCount     = await uow.Rooms.CountAsync();
        var bookingsCount  = await uow.Bookings.CountAsync();
        var allRooms       = await uow.Rooms.GetRoomWithBookingsAsync(null, null);
        var countOpen      = allRooms.Count(r => !r.CurrentlyAvailable);

        Console.WriteLine($"\n{customersCount} Hotelgäste, {roomsCount} Zimmer, {bookingsCount} Zimmerbuchungen wurden aus DB gelesen.");
        Console.WriteLine($"Davon sind {countOpen} Buchung offen.");
    }
}