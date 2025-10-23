namespace WinUIWpf.Views;

using System.Windows;

using WinUIWpf.ViewModels;

public partial class MovesWindow : Window
{
    public MovesWindow(MovesViewModel viewModel)
    {
        InitializeComponent();
        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as MovesViewModel)!
                .LoadDataAsync();
        };
    }
}