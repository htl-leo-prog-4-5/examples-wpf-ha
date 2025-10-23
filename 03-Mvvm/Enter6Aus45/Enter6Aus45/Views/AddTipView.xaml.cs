using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enter6Aus45.ViewModels;
using System.IO;
using System.Windows;

namespace Enter6Aus45.Views
{
    /// <summary>
    /// Interaction logic for AddTipView.xaml
    /// </summary>
    public partial class AddTipView : Window
    {
        public AddTipView()
        {
            InitializeComponent();
            var vm = (AddTipViewModel)DataContext;

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
