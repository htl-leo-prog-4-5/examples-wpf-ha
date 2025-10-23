namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        Loaded += async (v, e) =>
        {
            await (DataContext as MainWindowViewModel)!
                .InitializeDataAsync();
        };
    }
}