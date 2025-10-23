using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CheckFileNameWithICommand
{
	public class FileName : INotifyPropertyChanged
	{
		#region common

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			/*
						var PropertyChanged = this.PropertyChanged;
						if (PropertyChanged != null)
						{
							PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
						}
			*/
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
		{
			var memberExpression = (MemberExpression)projection.Body;
			OnPropertyChanged(memberExpression.Member.Name);
		}

		#endregion

		#region properties

		private string _filepath;

		public String FilePath
		{
			get
			{
				return _filepath;
			}
			set
			{
				_filepath = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region commands

		public bool CanIsFile()
		{
			return File.Exists(FilePath);
		}

		public void FileNameAction()
		{
			MessageBox.Show(FilePath);
		}

		public bool CanFileName(string param)
		{
			return CanIsFile();
		}

		public void FileNameCmd(string param)
		{
			MessageBox.Show($"{param}: {FilePath}");
		}

		#endregion

		#region ICommands

		public ICommand DoSomethingWithFile { get { return new DelegateCommand(FileNameAction,CanIsFile);  } }
		public ICommand DoSomethingWithFileWithParam { get { return new DelegateCommand<string>((string param) => MessageBox.Show(param + ": Hallo"), (string param) => CanIsFile()); } }
		public ICommand DoSomethingWithFileWithParam2 { get { return new DelegateCommand<string>(FileNameCmd,CanFileName); } }

		#endregion
	}
}
