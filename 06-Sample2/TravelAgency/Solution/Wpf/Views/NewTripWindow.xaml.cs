namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class NewTripWindow : Window
{
    public NewTripWindow(NewTripViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as NewTripViewModel)!
                .InitializeDataAsync();
        };
    }
}