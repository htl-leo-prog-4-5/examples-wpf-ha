namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for HikeHighlightsWindow.xaml
/// </summary>
public partial class HikeHighlightsWindow : Window
{
    public HikeHighlightsWindow(HikeHighlightsViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as HikeHighlightsViewModel)!
                .InitializeDataAsync();
        };
    }
}