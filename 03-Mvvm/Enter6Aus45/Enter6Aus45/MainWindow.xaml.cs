using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Enter6Aus45.ViewModels;
using Enter6Aus45.Views;

namespace Enter6Aus45
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

            vm.QuickTipDlg = () =>
            {
                var dlg = new AddTipView();
                (dlg.DataContext as AddTipViewModel).Filename = vm.FileName;
                dlg.ShowDialog();
            };
        }
    }
}
