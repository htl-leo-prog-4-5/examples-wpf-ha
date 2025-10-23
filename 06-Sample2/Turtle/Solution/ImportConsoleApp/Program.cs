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
        .AddTransient<IMoveRepository, MoveRepository>()
        .AddTransient<IScriptRepository, ScriptRepository>()
        .AddTransient<ICompetitionRepository, CompetitionRepository>()
        .AddTransient<IOriginRepository, OriginRepository>()
        .AddTransient<IVoteRepository, VoteRepository>()
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

    int countTotal = 0;

    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var importService = scope.ServiceProvider.GetRequiredService<IImportService>();
        var uow           = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var directories = Directory.GetDirectories("ImportData");
        foreach (var directory in directories)
        {
            var files = Directory.GetFiles(directory);

            foreach (var file in files)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var origin = Path.GetFileName(Path.GetDirectoryName(file));

                var date = DateOnly.FromDateTime(DateTime.Today);

                var dateStr = file.Substring(file.Length - 8 - 4, 8);
                if (dateStr.Length == 8 && dateStr.Any(char.IsDigit))
                {
                    try
                    {
                        date = new DateOnly(
                            int.Parse(dateStr.Substring(0, 4)),
                            int.Parse(dateStr.Substring(4, 2)),
                            int.Parse(dateStr.Substring(6, 2))
                        );
                    }
                    catch (FormatException)
                    {
                    }
                }

                await importService.ImportScriptAsync(Path.GetFileNameWithoutExtension(file), file, origin, date, file);

                stopwatch.Stop();

                Console.WriteLine($"Imported {file} in {stopwatch.Elapsed}");
                countTotal++;
            }
        }

        Console.WriteLine($"Import done: {countTotal} files");
    }
}