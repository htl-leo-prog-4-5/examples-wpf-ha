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
        .AddTransient<ITicketRepository, TicketRepository>()
        .AddTransient<IGameRepository, GameRepository>()
        .AddTransient<ITipRepository, TipRepository>()
        .AddTransient<IOfficeRepository, OfficeRepository>()
        .AddTransient<IStateRepository, StateRepository>()
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

        int nrOffice = await uow.OfficeRepository.CountAsync();
        int nrTip    = await uow.TipRepository.CountAsync();
        int nrTicket = await uow.TicketRepository.CountAsync();
        int nrGame   = await uow.GameRepository.CountAsync();
        Console.WriteLine($" {nrOffice} offices stored in DB");
        Console.WriteLine($" {nrTip} tips stored in DB");
        Console.WriteLine($" {nrTicket} tickets stored in DB");
        Console.WriteLine($" {nrGame} games stored in DB");
    }

    Console.WriteLine($"Import done");
}