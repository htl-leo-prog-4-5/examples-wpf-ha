namespace Wpf.Views;

using System.Windows;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for ScriptPreviewWindow.xaml
/// </summary>
public partial class ExaminationWindow : Window
{
    public ExaminationWindow(ExaminationViewModel viewModel)
    {
        InitializeComponent();

        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as ExaminationViewModel)!
                .InitializeDataAsync();
        };
    }
}