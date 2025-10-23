using System.Windows;

using WinUIWpf.ViewModels;

using WpfMvvmBase;


namespace WinUIWpf.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel            = AppService.GetRequiredService<ViewModels.MainWindowViewModel>();
        _viewModel.Controller = new WindowNavigator(this);
        DataContext           = _viewModel;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.LoadDataAsync();
    }
}