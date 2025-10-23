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

        //TODO: import.
        // import all subfolders from "ImportData"
        // => use the folder-name as origin and extract the date from the filename
        // => read all *.txt as a drawing in the sub folder
        await Task.CompletedTask;   // avoid cs1988

        Console.WriteLine($"Import done: {countTotal} files");
    }
}