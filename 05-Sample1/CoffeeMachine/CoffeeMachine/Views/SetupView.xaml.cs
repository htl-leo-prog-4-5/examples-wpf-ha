using CoffeeMachine.ViewModels;
using System.IO;
using System.Windows;

namespace CoffeeMachine.Views
{
    public partial class SetupView : Window
    {
        public SetupView()
        {
            InitializeComponent();

            var vm = (SetupViewModel) DataContext;

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
                var dir = Path.GetDirectoryName(filename);
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
