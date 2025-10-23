using System.Windows;

using WpfTadeotAdmin.ViewModels;

namespace WpfTadeotAdmin;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await ((MainViewModel)DataContext).LoadDataAsync();
    }
}