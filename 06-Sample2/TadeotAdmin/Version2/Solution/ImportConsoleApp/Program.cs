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
        AppService.ServiceCollection
            .AddScoped<ApplicationDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddTransient<ICityRepository, CityRepository>()
            .AddTransient<IDistrictRepository, DistrictRepository>()
            .AddTransient<IReasonForVisitRepository, ReasonForVisitRepository>()
            .AddTransient<IVisitorRepository, VisitorRepository>()
            .AddTransient<ISchoolTypeRepository, SchoolTypeRepository>()
            ;

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

        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            await uow.Cities.AddRangeAsync(cities);
            await uow.ReasonsForVisit.AddRangeAsync(reasons);
            await uow.SchoolTypes.AddRangeAsync(types);
            await uow.SaveChangesAsync();
        }

        Console.WriteLine($" {cities.Count(),5} Cities stored in DB");
        Console.WriteLine($" {reasons.Count(),5} ReasonsForVisit stored in DB");
        Console.WriteLine($" {types.Count(),5} SchoolTypes stored in DB");
    }

    private static async Task GenerateVisitorsAsync()
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            await uow.Visitors.GenerateTestDataAsync(400);
            int count = await uow.SaveChangesAsync();
            Console.WriteLine($" {count,5} visitors have been generated");
        }
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