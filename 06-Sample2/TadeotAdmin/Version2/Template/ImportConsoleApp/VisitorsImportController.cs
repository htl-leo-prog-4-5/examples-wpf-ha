using Core.Entities.Visitors;

using System.Text;

namespace ImportConsoleApp;

public class VisitorsImportController
{
    public const string DISTRICTS_CSV   = "csv-visitors/districts.csv";
    public const string CITIES_CSV      = "csv-visitors/cities.csv";
    public const string REASONS_CSV     = "csv-visitors/reasonsforvisit.csv";
    public const string SCHOOLTYPES_CSV = "csv-visitors/schooltypes.csv";

    public static async Task<IEnumerable<City>> ReadCitiesAsync()
    {
        throw new NotImplementedException();
    }

    public static async Task<IEnumerable<ReasonForVisit>> ReadReasonsAsync()
    {
        throw new NotImplementedException();
    }

    public static async Task<IEnumerable<SchoolType>> ReadSchoolTypesAsync()
    {
        throw new NotImplementedException();
    }
}