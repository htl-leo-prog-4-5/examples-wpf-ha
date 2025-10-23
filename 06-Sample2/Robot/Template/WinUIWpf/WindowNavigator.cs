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
}