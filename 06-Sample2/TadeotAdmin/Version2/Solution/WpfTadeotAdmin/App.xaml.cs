using Core.Contracts;
using Core.Contracts.Visitors;

using Microsoft.Extensions.DependencyInjection;

using Persistence;
using Persistence.Visitors;

using System.Windows;

using WpfTadeotAdmin.Views;

namespace WpfTadeotAdmin;

using Core;

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
                typeof(ViewModels.MainViewModel).Assembly)
            .AddScoped<ApplicationDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddTransient<ICityRepository, CityRepository>()
            .AddTransient<IDistrictRepository, DistrictRepository>()
            .AddTransient<IReasonForVisitRepository, ReasonForVisitRepository>()
            .AddTransient<IVisitorRepository, VisitorRepository>()
            .AddTransient<ISchoolTypeRepository, SchoolTypeRepository>()
            .AddTransient<MainWindow>()
            .AddTransient<RegistrationConfigWindow>();

        AppService.BuildServiceProvider();

        MainWindow = AppService.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }
}