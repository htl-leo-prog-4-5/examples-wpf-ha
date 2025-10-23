using Core.Entities.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportConsoleApp
{
    public class VisitorsImportController
    {
        public const string DISTRICTS_CSV = "csv-visitors/districts.csv";
        public const string CITIES_CSV = "csv-visitors/cities.csv";
        public const string REASONS_CSV = "csv-visitors/reasonsforvisit.csv";
        public const string SCHOOLTYPES_CSV = "csv-visitors/schooltypes.csv";

        public static async Task<IEnumerable<City>> ReadCitiesAsync()
        {
            var districtsCsv = (await File.ReadAllLinesAsync(DISTRICTS_CSV, Encoding.Default))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();
            var citiesCsv = (await File.ReadAllLinesAsync(CITIES_CSV, Encoding.Default))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();

            var districts = districtsCsv.Select(line => new
            {
                Name = line[0],
                Number = int.Parse(line[1])
            })
            .Distinct()
            .Select(o => new District
            {
                Name = o.Name,
                Number = o.Number
            })
            .ToList();

            var cities = citiesCsv.Select(line => new
            {
                DistrictNumber = int.Parse(line[2]) / 100,
                Name = line[1],
                ZipCode = line[3],
                Number = int.Parse(line[0])
            })
                .Distinct()
                .Select(o => new City
                {
                    District = districts.SingleOrDefault(d => d.Number == o.DistrictNumber),
                    Name = o.Name,
                    ZipCode = o.ZipCode,
                    Number = o.Number
                })
                .ToList();

            var citiesCount = cities.GroupBy(c => c.Name).Count();
            var districtsCount = cities.GroupBy(c => c.District).Count();
            var zipCount = cities.GroupBy(c => c.ZipCode).Count();

            return cities;
        }

        public static async Task<IEnumerable<ReasonForVisit>> ReadReasonsAsync()
        {
            var reasonsCsv = (await File.ReadAllLinesAsync(REASONS_CSV, Encoding.Default))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();

            var reasons = reasonsCsv.Select(line => new ReasonForVisit
            {
                Reason = line[1],
                Rank = Int32.Parse(line[0])

            }).
            OrderBy(o => o.Rank)
            .ToList();

            return reasons;
        }

        public static async Task<IEnumerable<SchoolType>> ReadSchoolTypesAsync()
        {
            var typesCsv = (await File.ReadAllLinesAsync(SCHOOLTYPES_CSV, Encoding.Default))
                .Skip(1)
                .Select(l => l.Split(";"))
                .ToList();

            var types = typesCsv.Select(line => new SchoolType
            {
                Type = line[1],
                Rank = Int32.Parse(line[0])

            }).
            OrderBy(o => o.Rank)
            .ToList();

            return types;
        }


    }
}
