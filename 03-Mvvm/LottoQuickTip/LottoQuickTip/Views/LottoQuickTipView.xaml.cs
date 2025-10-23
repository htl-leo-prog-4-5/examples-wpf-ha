using LottoQuickTip.ViewModels;
using System.IO;
using System.Windows;

namespace LottoQuickTip.Views
{
    /// <summary>
    /// Interaction logic for LottoQuickTipView.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LottoQuickTipViewModel vm = (LottoQuickTipViewModel) DataContext;

            vm.CloseAction = () => { Close(); };

            vm.BrowseFileNameFunc = (filename, savefile) =>
            {
                Microsoft.Win32.FileDialog dlg;
                if (savefile)
                {
                    dlg = new Microsoft.Win32.SaveFileDialog();
                }
                else
                {
                    dlg = new Microsoft.Win32.OpenFileDialog();
                }
                dlg.FileName = filename;
                string dir = Path.GetDirectoryName(filename);
                if (!string.IsNullOrEmpty(dir))
                {
                    dlg.InitialDirectory = dir;
                    dlg.FileName = Path.GetFileName(filename);
                }

                if ((dlg.ShowDialog() ?? false))
                {
                    return dlg.FileName;
                }
                return null;
            };
        }
    }
}
