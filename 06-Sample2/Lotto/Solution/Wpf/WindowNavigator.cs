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

    public async Task ShowGameDetailWindowAsync(Game game)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<GameDetailWindow>();
            var vm     = (GameDetailViewModel) window.DataContext;
            vm.GameId = game.Id;
            window.ShowDialog();
            await Task.CompletedTask;
        }
    }

    public async Task ShowDrawWindowAsync(Game game)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<DrawGameWindow>();
            var vm     = (DrawGameViewModel)window.DataContext;
            vm.GameId = game.Id;
            window.ShowDialog();
            await Task.CompletedTask;
        }
    }

    public async Task ShowNewGameWindowAsync()
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<NewGameWindow>();
            var vm     = (NewGameViewModel)window.DataContext;
            window.ShowDialog();
            await Task.CompletedTask;
        }
    }
}