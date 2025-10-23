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
        .AddTransient<IExaminationRepository, ExaminationRepository>()
        .AddTransient<IDoctorRepository, DoctorRepository>()
        .AddTransient<IPatientRepository, PatientRepository>()
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

//        if (true)
        {
            Console.WriteLine("Creating database ...");
            await uow.CreateDatabaseAsync();
        }
/*
        else
        {
            Console.WriteLine("Recreating and migrating database ...");
            await uow.MigrateDatabaseAsync();
        }
*/
    }
}

async Task Import()
{
    Console.WriteLine("=====================");
    Console.WriteLine("Import");

    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var importService = scope.ServiceProvider.GetRequiredService<IImportService>();

        var stopwatch = new Stopwatch();

        stopwatch.Start();

        int doctors = await importService.ImportDoctorsAsync("ImportData/BaseData/Doctors.txt");

        int count = await importService.ImportAsync("ImportData");

        stopwatch.Stop();

        Console.WriteLine($"Imported {count} files(examinations) in {stopwatch.Elapsed}");
        Console.WriteLine($"Imported {doctors} doctors");
    }
}