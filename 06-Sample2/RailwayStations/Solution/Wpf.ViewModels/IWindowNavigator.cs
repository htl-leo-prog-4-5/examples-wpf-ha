namespace Wpf.ViewModels;

using Base.WpfMvvm;

using Core.Entities;

public interface IWindowNavigator : IBaseWindowNavigator
{
    public Task ShowStationWindowAsync(Station fame);
}