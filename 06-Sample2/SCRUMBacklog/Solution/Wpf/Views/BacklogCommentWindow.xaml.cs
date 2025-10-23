namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for BacklogCommentWindow.xaml
/// </summary>
public partial class BacklogCommentWindow : Window
{
    public BacklogCommentWindow(BacklogCommentViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as BacklogCommentViewModel)!
                .InitializeDataAsync();
        };
    }
}