using System.Windows;
using Wpf.ViewModels;

namespace Wpf.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new MainViewModel();
            DataContext = viewModel;

            viewModel.Controller = new WindowNavigator(this);

            Loaded += async (v, e) =>
            {
                await (DataContext as MainViewModel)!
                    .InitializeDataAsync();
            };
        }
    }
}