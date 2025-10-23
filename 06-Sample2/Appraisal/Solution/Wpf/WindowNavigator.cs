namespace Wpf;

using System.Windows;

using Base.Core;
using Base.WpfMvvm;

using Microsoft.Extensions.DependencyInjection;

using Wpf.ViewModels;
using Wpf.Views;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }

    public async Task ShowExaminationsWindowAsync(int patientId)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<ExaminationWindow>();
            var vm     = (ExaminationViewModel)window.DataContext;
            vm.PatientId = patientId;
            window.ShowDialog();
            await Task.CompletedTask;
        }
    }
}