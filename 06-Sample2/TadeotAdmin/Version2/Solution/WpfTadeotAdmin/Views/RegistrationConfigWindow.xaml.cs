using System.Windows;

using WpfTadeotAdmin.ViewModels;

namespace WpfTadeotAdmin.Views;

/// <summary>
/// Interaction logic for RegistrationConfigWindow.xaml
/// </summary>
public partial class RegistrationConfigWindow : Window
{
    public RegistrationConfigWindow(RegistrationConfigViewModel viewModel)
    {
        InitializeComponent();
        DataContext          = viewModel;
        viewModel.Controller = new WindowNavigator(this);
    }
    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await ((RegistrationConfigViewModel)DataContext).LoadDataAsync();
    }
}