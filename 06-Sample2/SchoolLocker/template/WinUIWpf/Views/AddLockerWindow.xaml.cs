using System.Windows;

namespace WinUIWpf.Views;

using WinUIWpf.ViewModels;

/// <summary>
/// Interaction logic for AddLockerWindow.xaml
/// </summary>
public partial class AddLockerWindow : Window
{
    public AddLockerWindow(AddLockerWindowViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;
    }
}