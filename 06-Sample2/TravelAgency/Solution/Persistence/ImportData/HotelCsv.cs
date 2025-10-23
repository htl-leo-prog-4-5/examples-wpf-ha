namespace Persistence.ImportData;

using Base.Tools.CsvImport;

public class HotelCsv
{
    public required string  Name          { get; set; }
    public required string  Location      { get; set; }
    public          decimal PricePerNight { get; set; }
    public          decimal Rating        { get; set; }
}