namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for ImportTurtleWindow.xaml
/// </summary>
public partial class ImportTurtleWindow : Window
{
    public ImportTurtleWindow(ImportTurtleViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as ImportTurtleViewModel)!
                .InitializeDataAsync();
        };
    }
}