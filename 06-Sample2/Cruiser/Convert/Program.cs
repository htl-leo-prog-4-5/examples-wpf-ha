
using Base.Tools.CsvImport;
using Convert;

var schiffe = await (new CsvImport<CruiserCsv>().ReadAsync("Schiffe.txt"));

foreach (var b in schiffe)
{
    Console.WriteLine(b);
}
// print all csv entries