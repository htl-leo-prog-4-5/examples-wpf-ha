using System.Windows;

namespace WinUIWpf.Views;

using WinUIWpf.ViewModels;

/// <summary>
/// Interaction logic for AddLockerWindow.xaml
/// </summary>
public partial class ShowBookingWindow : Window
{
    public ShowBookingWindow(ShowBookingWindowViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await ((ShowBookingWindowViewModel)DataContext).LoadBookingAsync();
    }
}