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
        string[][] matrix = (await File.ReadAllLinesAsync(Filename, Encoding.Default))
            .Skip(1)
            .Select(s => s.Split(";"))
            .ToArray();
        
        var lockers = matrix
            .GroupBy(line => line[2])
            .Select(grp => new Locker { Number = int.Parse(grp.Key) })
            .ToDictionary(l => l.Number);
        
        var pupils = matrix
            .GroupBy(line => (line[0],line[1]))
            .Select(grp => new Pupil { LastName = grp.First()[0], FirstName = grp.First()[1] })
            .ToDictionary(p => (p.LastName, p.FirstName));
        
        var bookings = matrix.Select(line => new Booking
        {
            Pupil  = pupils[(line[0], line[1])],
            Locker = lockers[int.Parse(line[2])],
            From   = DateTime.Parse(line[3]),
            To     = line[4].Length > 0 ? DateTime.Parse(line[4]) : (DateTime?)null
        }).ToList();
        return bookings;
    }
}