namespace Wpf.ViewModels;

using Base.WpfMvvm;

public interface IWindowNavigator : IBaseWindowNavigator
{
    public Task ShowExaminationsWindowAsync(int patientId);
}