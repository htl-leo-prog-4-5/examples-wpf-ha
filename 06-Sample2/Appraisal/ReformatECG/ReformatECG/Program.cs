using System.Globalization;
using Base.Tools.CsvImport;
using ReformatECG;

const string StreamName = "LeadIII";

var import = new CsvImport<ECGCsv>()
{
    ListSeparatorChar = ',',
    MapColumns = new Dictionary<string, string>()
    {
        {"Time (s)", nameof(ECGCsv.Time)},
        {"Voltage (mV)", nameof(ECGCsv.Voltage)},
        {"Lead", nameof(ECGCsv.Name)}
    }
};

await Export("sample_ecg.csv", "Stream1");
await Export("realistic_ecg.csv", "LeadI");
await Export("realistic_ecg_same_hr.csv", "LeadII");
await Export("realistic_ecg_variant.csv", "LeadIV");

async Task Export(string filename, string streamName)
{
    var csv = await import.ReadAsync(filename);


    var lines = Split(csv, 10);

    var result = lines.Select((l, idx) =>
        $"{streamName}Data{idx:0000};{string.Join(',', l.Select(c => c.Voltage.ToString(CultureInfo.InvariantCulture)))}");

    var streamInfo = new List<string>
    {
        $"{streamName}Name;{csv[2].Name}",
        $"{streamName}Period;{csv[2].Time.ToString(CultureInfo.InvariantCulture)}",
    };

    streamInfo.AddRange(result);

    File.WriteAllLines($"result{streamName}.txt", streamInfo);
}

Console.WriteLine("done");


IEnumerable<IEnumerable<T>> Split<T>(IEnumerable<T> list, int size)
{
    if (size < 1)
    {
        throw new ArgumentOutOfRangeException(nameof(size), size, @"Must not be < 1");
    }

    var listList = new List<IEnumerable<T>>();
    var count = 0;
    var lastList = new List<T>();

    foreach (var element in list)
    {
        if ((count % size) == 0)
        {
            lastList = new List<T>();
            listList.Add(lastList);
        }

        lastList.Add(element);
        count++;
    }

    return listList;
}