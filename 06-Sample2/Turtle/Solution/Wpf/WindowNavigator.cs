namespace Wpf;

using System.Windows;

using Base.Core;
using Base.WpfMvvm;

using Microsoft.Extensions.DependencyInjection;

using Wpf.ViewModels;
using Wpf.Views;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }

    public async Task ShowBacklogCommentWindowAsync(int backlogItemId)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<ScriptPreviewWindow>();
            var vm     = (ScriptPreviewViewModel)window.DataContext;
            vm.ScriptId = backlogItemId;
            window.ShowDialog();
            await Task.CompletedTask;
        }
    }

    public async Task ShowImportTurtleWindowAsync()
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var window = scope.ServiceProvider.GetRequiredService<ImportTurtleWindow>();
            window.ShowDialog();
            await Task.CompletedTask;
        }
    }
}