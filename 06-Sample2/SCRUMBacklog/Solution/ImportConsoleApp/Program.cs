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
        .AddTransient<ICommentRepository, CommentRepository>()
        .AddTransient<IBacklogItemRepository, BacklogItemRepository>()
        .AddTransient<IEffortRepository, EffortRepository>()
        .AddTransient<ITeamMemberRepository, TeamMemberRepository>()
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

        await importService.ImportDbAsync();

        int nrBacklogItems = await uow.BacklogItemRepository.CountAsync();
        int nrTeamMembers   = await uow.TeamMemberRepository.CountAsync();
        int nrEfforts      = await uow.EffortRepository.CountAsync();
        int nrComments       = await uow.CommentRepository.CountAsync();

        Console.WriteLine($" {nrBacklogItems} backlogItems stored in DB");
        Console.WriteLine($" {nrTeamMembers} team members in DB");
        Console.WriteLine($" {nrEfforts} efforts stored in DB");
        Console.WriteLine($" {nrComments} comments in DB");
    }

    Console.WriteLine($"Import done");
}