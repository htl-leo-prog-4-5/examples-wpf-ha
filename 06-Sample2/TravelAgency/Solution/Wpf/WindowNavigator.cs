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

    public async Task ShowNewTripWindowAsync()
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<NewTripWindow>();
            var vm     = (NewTripViewModel)window.DataContext;
            window.ShowDialog();

            await Task.CompletedTask;
        }
    }

    public async Task ShowTripWindowAsync(Trip trip)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<TripWindow>();
            var vm     = (TripViewModel)window.DataContext;
            vm.TripId = trip.Id;

            window.ShowDialog();

            await Task.CompletedTask;
        }
    }
}