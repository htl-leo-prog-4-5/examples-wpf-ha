using Core.Contracts.Visitors;
using Core.Contracts;

using Microsoft.Extensions.DependencyInjection;

using Persistence;
using Persistence.Visitors;

namespace ImportConsoleApp;

using Core;

internal class Program
{
    static async Task Main()
    {
        AppService.ServiceCollection = new ServiceCollection();

        // TODO: DI configuration for Import
/*
        AppService.ServiceCollection
            .AddScoped<ApplicationDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddTransient<ICityRepository, CityRepository>()
            ;
*/
        AppService.BuildServiceProvider();

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

        Console.WriteLine($" {cities.Count(),5} Cities stored in DB");
        Console.WriteLine($" {reasons.Count(),5} ReasonsForVisit stored in DB");
        Console.WriteLine($" {types.Count(),5} SchoolTypes stored in DB");
    }

    private static async Task GenerateVisitorsAsync()
    {
    }

    private static async Task RecreateDatabaseAsync()
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            Console.WriteLine("Deleting database ...");
            await uow.DeleteDatabaseAsync();

            Console.WriteLine("Recreating and migrating database ...");
            await uow.MigrateDatabaseAsync();
        }
    }
}