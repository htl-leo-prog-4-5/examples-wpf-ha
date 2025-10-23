using Persistence;

namespace ImportConsoleApp
{
    internal class Program
    {
        static async Task Main()
        {
            await RecreateDatabaseAsync();
            
            await ImportVisitorDataAsync();
            await GenerateVisitorsAsync();

            Console.WriteLine("\n\nPress any key to exit ...");
            Console.ReadKey();
        }

        private static async Task ImportVisitorDataAsync()
        {
            Console.WriteLine();
            Console.WriteLine("Reading cities and districts from csv file ...");
            var cities = await VisitorsImportController.ReadCitiesAsync();
            Console.WriteLine("Reading reasons for visits from csv file ...");
            var reasons = await VisitorsImportController.ReadReasonsAsync();
            Console.WriteLine("Reading school types from csv file ...");
            var types = await VisitorsImportController.ReadSchoolTypesAsync();

            // TODO: store collections into db
            throw new NotImplementedException();

            //Console.WriteLine($" {cities.Count(),5} Cities stored in DB");
            //Console.WriteLine($" {reasons.Count(),5} ReasonsForVisit stored in DB");
            //Console.WriteLine($" {types.Count(),5} SchoolTypes stored in DB");
        }

        private static Task GenerateVisitorsAsync()
        {
            // TODO: Generate and import fake visitor data
            // (Use Visitor repository)
            throw new NotImplementedException();

            // Console.WriteLine($"  count,5} visitors have been generated");
        }

        private static async Task RecreateDatabaseAsync()
        {
            await using var uow = new UnitOfWork();
            Console.WriteLine("Deleting database ...");
            await uow.DeleteDatabaseAsync();

            Console.WriteLine("Recreating and migrating database ...");
            await uow.MigrateDatabaseAsync();
        }
    }
}