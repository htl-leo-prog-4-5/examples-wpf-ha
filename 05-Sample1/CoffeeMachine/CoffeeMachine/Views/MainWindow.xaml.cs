using System.Windows;
using CoffeeMachine.ViewModels;

namespace CoffeeMachine.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = (MainWindowViewModel)DataContext;

            vm.CloseAction = () => { Close(); };

            vm.ShowSetupDlg = () =>
            {
                var dlg = new SetupView();
                dlg.ShowDialog();
            };
            vm.SellSetupDlg = () =>
            {
                var dlg = new SellView();
                dlg.ShowDialog();
            };
        }
    }
}
