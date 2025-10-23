namespace Wpf;

using System.Windows;

using Base.Core;
using Base.WpfMvvm;

using Microsoft.Extensions.DependencyInjection;

using Wpf.ViewModels;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }
}