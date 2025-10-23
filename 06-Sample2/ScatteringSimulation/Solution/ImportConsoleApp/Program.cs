using Base.Core;
using Base.Tools;

using Core.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence;

using System.Diagnostics;

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
        .AddTransient<ISampleRepository, SampleRepository>()
        .AddTransient<ISimulationRepository, SimulationRepository>()
        .AddTransient<ICategoryRepository, CategoryRepository>()
        .AddTransient<IOriginRepository, OriginRepository>()
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

        if (true)
        {
            Console.WriteLine("Creating database ...");
            await uow.CreateDatabaseAsync();
        }
        else
        {
            Console.WriteLine("Recreating and migrating database ...");
            await uow.MigrateDatabaseAsync();
        }
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

        await importService.ImportBaseDataAsync();

        int nrSimulations = await uow.SimulationRepository.CountAsync();
        int nrOrigins     = await uow.OriginRepository.CountAsync();
        int nrCategories  = await uow.CategoryRepository.CountAsync();
        int nrSamples     = await uow.SampleRepository.CountAsync();

        Console.WriteLine($" {nrSimulations} simulations stored in DB");
        Console.WriteLine($" {nrOrigins} origins stored in DB");
        Console.WriteLine($" {nrCategories} categories stored in DB");
        Console.WriteLine($" {nrSamples} samples in DB");
    }

    int countTotal = 0;

    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var importService = scope.ServiceProvider.GetRequiredService<IImportService>();
        var uow           = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var csvFiles = Directory.GetFiles("ImportData", "*_Info.txt");

        foreach (var file in csvFiles)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await importService.ImportSampleAsync(file);

            stopwatch.Stop();

            Console.WriteLine($"Imported {file} in {stopwatch.Elapsed}");
            countTotal++;
        }

        Console.WriteLine($"Import done: {countTotal} files");
    }


    Console.WriteLine($"Import done");
}