namespace Wpf;

using System.Windows;

using Base.WpfMvvm;

using Wpf.ViewModels;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }
/*
    public async Task ShowHikeHighlightsWindowAsync(int hikeId)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope)
        {
            var window = scope.ServiceProvider.GetRequiredService<HikeHighlightsWindow>();

        }
    }
*/
}