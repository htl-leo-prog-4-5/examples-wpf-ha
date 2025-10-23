namespace WinUIWpf;

using System.Windows;

using Base.Core;
using Base.WpfMvvm;

using Core.Entities;
using Core.QueryResults;

using Microsoft.Extensions.DependencyInjection;

using WinUIWpf.ViewModels;
using WinUIWpf.Views;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }

    public void ShowRacesWindow(CompetitionSummary competition)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var racesWindow = scope.ServiceProvider.GetRequiredService<RacesWindow>();
            var racesVm     = racesWindow.DataContext as RacesViewModel;
            racesVm!.CompetitionSummary = competition;
            racesWindow.ShowDialog();
        }
    }
    
    public void ShowMovesWindow(Race race)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var movesWindow = scope.ServiceProvider.GetRequiredService<MovesWindow>();
            var movesVm     = movesWindow.DataContext as MovesViewModel;
            movesVm!.Race = race;
            movesWindow.ShowDialog();
        }
    }

}