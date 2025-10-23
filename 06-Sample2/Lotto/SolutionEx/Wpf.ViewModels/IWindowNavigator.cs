namespace Wpf.ViewModels;

using Base.WpfMvvm;

using Core.DataTransferObjects;
using Core.Entities;

public interface IWindowNavigator : IBaseWindowNavigator
{
    public Task ShowGameDetailWindowAsync(Game fame);

    public Task ShowDrawWindowAsync(Game fame);

    public Task ShowNewGameWindowAsync();
}