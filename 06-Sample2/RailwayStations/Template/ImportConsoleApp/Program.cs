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
        .AddTransient<IRailwayCompanyRepository, RailwayCompanyRepository>()
        .AddTransient<IStationRepository, StationRepository>()
        .AddTransient<ICityRepository, CityRepository>()
        .AddTransient<IInfrastructureRepository, InfrastructureRepository>()
        .AddTransient<ILineRepository, LineRepository>()
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

//        Console.WriteLine("Recreating and migrating database ...");
//        await uow.CreateDatabaseAsync();

        Console.WriteLine("Recreating and migrating database ...");
        await uow.MigrateDatabaseAsync();
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

        int nrInfrastructure = await uow.InfrastructureRepository.CountAsync();
        int nrcities   = await uow.CityRepository.CountAsync();
        int nrCompanies      = await uow.RailwayCompanyRepository.CountAsync();
        int nrStations       = await uow.StationRepository.CountAsync();
        int nrLines       = await uow.LineRepository.CountAsync();

        Console.WriteLine($" {nrInfrastructure} infrastructure companies stored in DB");
        Console.WriteLine($" {nrcities} cities stored in DB");
        Console.WriteLine($" {nrCompanies} railway companies stored in DB");
        Console.WriteLine($" {nrStations} stations stored in DB");
        Console.WriteLine($" {nrLines} lines stored in DB");
    }

    Console.WriteLine($"Import done");
}