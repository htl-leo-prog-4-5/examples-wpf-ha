using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace BrowserCustomControl
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _filename = "File.txt";
        public string FileName
        {
            get => _filename;
            set
            {
                _filename = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SelectFileNameCommand => new DelegateCommand(() =>
            {
                var dlg = new OpenFileDialog();
                var res = dlg.ShowDialog();
                if (res??false)
                {
                    FileName = dlg.FileName;
                }
            }
        );
        public ICommand ClearFilenameCommand => new DelegateCommand(() => FileName = "");
        public ICommand SetFilenameCommand => new DelegateCommand(() => FileName = "Hallo.txt");
    }
}
