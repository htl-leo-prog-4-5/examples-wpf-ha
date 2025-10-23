using Persistence;

namespace ImportConsole;

internal class Program
{
    static async Task Main()
    {
        await InitDataAsync();

        Console.WriteLine();
        Console.Write("Beenden mit Eingabetaste ...");
        Console.ReadLine();
    }

    private static async Task InitDataAsync()
    {
        await using var unitOfWork = new UnitOfWork();
        Console.WriteLine("Datenbank löschen");
        await unitOfWork.DeleteDatabaseAsync();
        Console.WriteLine("Datenbank migrieren");
        await unitOfWork.MigrateDatabaseAsync();
        Console.WriteLine("Buchungen werden von schoollocker.csv eingelesen");
        var bookings = await ImportController.ReadFromCsvAsync();
        if (bookings.Count() == 0)
        {
            Console.WriteLine("!!! Es wurden keine Buchungen eingelesen");
            return;
        }

        Console.WriteLine(
            $"  Es wurden {bookings.Count()} Buchungen eingelesen, werden in Datenbank gespeichert ...");
        await unitOfWork.Bookings.AddRangeAsync(bookings);
        int countPupils  = bookings.GroupBy(b => b.Pupil).Count();
        int countLockers = bookings.GroupBy(b => b.Locker).Count();
        int savedRows    = await unitOfWork.SaveChangesAsync();
        Console.WriteLine(
            $"{countLockers} Spinde, {countPupils} Schüler und {savedRows - countLockers - countPupils} Buchungen wurden in Datenbank gespeichert!");
        Console.WriteLine();
    }
}