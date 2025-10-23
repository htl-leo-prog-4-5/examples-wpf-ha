using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Enter6Aus45.Helpers;

namespace Enter6Aus45.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		#region INPC 

		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        #endregion

        #region GUI forward

        public Func<string, bool> MsgBox { get; set; }
        public Action CloseAction { get; set; }
        public Action QuickTipDlg { get; set; }

        #endregion

        #region Properties


        string _filenname = @"c:\tmp\Enter6Aus45.csv";
		public string FileName
		{
			get => _filenname;
		    set { _filenname = value; RaisePropertyChanged(); }
		}

		#endregion

		#region Operations

		public void CreateEmpty()
        {
            File.Create(FileName);
        }

        public bool FileExists()
		{
			return string.IsNullOrEmpty(FileName) == false && File.Exists(FileName);
		}
		public void AddTip()
		{
            QuickTipDlg?.Invoke();

        }
		public bool CanAddTip()
        {
            return false;
        }

        #endregion

        #region Commands

        public ICommand CreateEmptyCommand => new DelegateCommand(CreateEmpty, () => !FileExists());
		public ICommand AddTipCommand => new DelegateCommand(AddTip, FileExists);

		#endregion

	}
}
