namespace WpfApp.Views;

using System.Windows;

using WinUIWpf.ViewModels;

using WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel       = new MainWindowViewModel(new WindowNavigator(this));
        this.DataContext = _viewModel;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.LoadDataAsync();
    }
}