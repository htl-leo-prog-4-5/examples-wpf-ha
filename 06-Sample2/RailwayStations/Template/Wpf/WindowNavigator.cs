namespace Wpf;

using System.Windows;

using Base.Core;
using Base.WpfMvvm;

using Core.Entities;

using Microsoft.Extensions.DependencyInjection;

using Wpf.ViewModels;
using Wpf.Views;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }

    public async Task ShowStationWindowAsync(Station station)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<StationWindow>();
            var vm     = (StationViewModel)window.DataContext;
            window.ShowDialog();
            await Task.CompletedTask;
        }
    }
}