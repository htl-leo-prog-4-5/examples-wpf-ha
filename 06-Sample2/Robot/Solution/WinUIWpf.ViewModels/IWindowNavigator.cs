namespace WinUIWpf.ViewModels;

using Base.WpfMvvm;

using Core.Entities;
using Core.QueryResults;

public interface IWindowNavigator : IBaseWindowNavigator
{
    public void ShowRacesWindow(CompetitionSummary competition);
    public void ShowMovesWindow(Race race);
}