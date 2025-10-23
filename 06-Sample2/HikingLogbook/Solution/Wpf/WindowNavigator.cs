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

    public async Task ShowHikeHighlightsWindowAsync(int hikeId)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<HikeHighlightsWindow>();
            var vm     = (HikeHighlightsViewModel)window.DataContext;
            vm.HikeId = hikeId;
            window.ShowDialog();
            await Task.CompletedTask;
        }
    }
}