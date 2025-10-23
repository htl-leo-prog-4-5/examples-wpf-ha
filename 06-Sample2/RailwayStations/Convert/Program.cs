
using Base.Tools.CsvImport;
using Convert;

var bahnhoefe = await (new CsvImport<StationEntry>().ReadAsync("Bahnhöfe.txt"));

foreach (var b in bahnhoefe)
{
    Console.WriteLine(b);
}
// print all csv entries