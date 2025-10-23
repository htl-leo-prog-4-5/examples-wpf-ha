using System.Windows;

namespace Wpf;

using Base.Core;
using Base.Tools;

using Core.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence;

using Wpf.Views;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var configuration = ConfigurationHelper.GetConfiguration();

        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        AppService.ServiceCollection = new ServiceCollection();
        AppService.ServiceCollection
            .AddSingleton(configuration)
            .AddAssemblyByName(
                n => n.EndsWith("ViewModel"),
                ServiceLifetime.Transient,
                typeof(ViewModels.MainWindowViewModel).Assembly)
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddTransient<ITicketRepository, TicketRepository>()
            .AddTransient<IGameRepository, GameRepository>()
            .AddTransient<ITipRepository, TipRepository>()
            .AddTransient<IOfficeRepository, OfficeRepository>()
            .AddTransient<MainWindow>()
            .AddTransient<GameDetailWindow>()
            .AddTransient<NewGameWindow>()
            .AddTransient<DrawGameWindow>()
            ;

        AppService.BuildServiceProvider();

        MainWindow = AppService.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }
}