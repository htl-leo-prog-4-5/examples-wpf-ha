namespace WinUIWpf;

using System.Windows;

using Core.Contracts;

using Microsoft.Extensions.DependencyInjection;

using Persistence;

using WinUIWpf.Views;

using WpfMvvmBase;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void AppStartup(object sender, StartupEventArgs e)
    {
        AppService.ServiceCollection = new ServiceCollection();
        AppService.ServiceCollection
            .AddAssemblyByName(
                n => n.EndsWith("ViewModel"),
                ServiceLifetime.Transient,
                typeof(ViewModels.MainWindowViewModel).Assembly)
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddTransient<MainWindow>()
            .AddTransient<AddLockerWindow>()
            .AddTransient<ShowBookingWindow>();

        AppService.BuildServiceProvider();

        MainWindow = AppService.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }
}