using Base.Core;
using Base.Tools;

using Core.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence;

ConfigureDependencyInjector();
await RecreateDatabaseAsync();
await Import();

void ConfigureDependencyInjector()
{
    var configuration = ConfigurationHelper.GetConfiguration();

    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    AppService.ServiceCollection = new ServiceCollection();
    AppService.ServiceCollection
        .AddSingleton(configuration)
        .AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString))
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddTransient<ITripRepository, TripRepository>()
        .AddTransient<IRouteRepository, RouteRepository>()
        .AddTransient<IRouteStepRepository, RouteStepRepository>()
        .AddTransient<IHotelRepository, HotelRepository>()
        .AddTransient<IPlaneRepository, PlaneRepository>()
        .AddTransient<IShipRepository, ShipRepository>()
        .AddTransient<IImportService, ImportService>()
        ;

    AppService.BuildServiceProvider();
}

async Task RecreateDatabaseAsync()
{
    Console.WriteLine("=====================");
    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        Console.WriteLine("Deleting database ...");
        await uow.DeleteDatabaseAsync();

        Console.WriteLine("Recreating and migrating database ...");
        await uow.CreateDatabaseAsync();
        //await uow.MigrateDatabaseAsync();
    }
}

async Task Import()
{
    Console.WriteLine("=====================");
    Console.WriteLine("Import");

    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var importService = scope.ServiceProvider.GetRequiredService<IImportService>();
        var uow           = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await importService.ImportDbAsync();

        int nrTrip      = await uow.TripRepository.CountAsync();
        int nrRoute     = await uow.RouteRepository.CountAsync();
        int nrRouteStep = await uow.RouteStepRepository.CountAsync();
        int nrHotel     = await uow.HotelRepository.CountAsync();
        int nrShip      = await uow.ShipRepository.CountAsync();
        int nrPlane     = await uow.PlaneRepository.CountAsync();

        Console.WriteLine($" {nrTrip} trips stored in DB");
        Console.WriteLine($" {nrRoute} routes stored in DB");
        Console.WriteLine($" {nrRouteStep} route-steps stored in DB");
        Console.WriteLine($" {nrHotel} hotels stored in DB");
        Console.WriteLine($" {nrShip} ships stored in DB");
        Console.WriteLine($" {nrPlane} planes stored in DB");
    }

    Console.WriteLine($"Import done");
}