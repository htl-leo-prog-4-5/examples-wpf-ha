namespace WinUIWpf.Views;

using System.Windows;

using WinUIWpf.ViewModels;

public partial class RacesWindow : Window
{
    public RacesWindow(RacesViewModel viewModel)
    {
        InitializeComponent();
        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as RacesViewModel)!
                .LoadDataAsync();
        };
    }
}