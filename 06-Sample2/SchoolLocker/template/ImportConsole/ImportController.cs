using System.Text;

using Core.Entities;

namespace ImportConsole;

public class ImportController
{
    const string Filename = "schoollocker.csv";

    /// <summary>
    /// Liefert die Buchungen mit den dazugehörigen Schülern und Spinden
    /// </summary>
    public async static Task<IEnumerable<Booking>> ReadFromCsvAsync()
    {
        //TODO read csv into list of Bookings 
        throw new NotImplementedException();
    }
}