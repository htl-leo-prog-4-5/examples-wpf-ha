namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class StationWindow : Window
{
    public StationWindow(StationViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as StationViewModel)!
                .InitializeDataAsync();
        };
    }
}