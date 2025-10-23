namespace Wpf;

using System.Windows;

using Base.WpfMvvm;

using Wpf.ViewModels;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }
}