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

        public static Task<IEnumerable<City>> ReadCitiesAsync()
        {
            // TODO: Read cities with districts from csv
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<ReasonForVisit>> ReadReasonsAsync()
        {
            // TODO: Read reasons for visits from csv
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<SchoolType>> ReadSchoolTypesAsync()
        {
            // TODO: Read school types from CSV
            throw new NotImplementedException();
        }
    }
}
