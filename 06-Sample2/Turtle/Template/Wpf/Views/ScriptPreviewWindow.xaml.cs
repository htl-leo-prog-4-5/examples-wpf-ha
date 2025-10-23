namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for ScriptPreviewWindow.xaml
/// </summary>
public partial class ScriptPreviewWindow : Window
{
    public ScriptPreviewWindow(ScriptPreviewViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as ScriptPreviewViewModel)!
                .InitializeDataAsync();
        };
    }
}