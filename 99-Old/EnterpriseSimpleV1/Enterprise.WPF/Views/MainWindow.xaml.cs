using System.Windows;

using Enterprise.Logic.Contracts;
using Enterprise.Shared;
using Enterprise.WPF.ViewModels;

using Unity;
using Unity.Extension;

namespace Enterprise.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = Dependency.Resolve<MainWindowsViewModel>();
        }
    }
}