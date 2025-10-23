namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for SimulationPreviewWindow.xaml
/// </summary>
public partial class SimulationPreviewWindow : Window
{
    public SimulationPreviewWindow(SimulationPreviewViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as SimulationPreviewViewModel)!
                .InitializeDataAsync();
        };
    }
}