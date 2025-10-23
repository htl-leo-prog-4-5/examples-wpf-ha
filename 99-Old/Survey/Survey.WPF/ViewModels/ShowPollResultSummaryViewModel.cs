using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Survey.Wpf.Helpers;

namespace Survey.Wpf.ViewModels
{
	public class ShowPollResultSummaryViewModel : Prism.Mvvm.BindableBase
	{
		#region Properties
		public Action CloseAction { get; set; }

		ObservableCollection<DTO.PollResultSummary> _pollresult = new ObservableCollection<DTO.PollResultSummary>();
		public ObservableCollection<DTO.PollResultSummary> Results
		{
			get { return _pollresult; }
			set { _pollresult = value; OnPropertyChanged(); }
		}


		#endregion


		#region Operations

		public async void Load()
		{
			var sp = new Survey.ServiceProxy.ServiceProxy();
			var results = await sp.GetResultsSummary();
			Results.Clear();
			foreach (var r in results)
				Results.Add(r);
		}

		bool CanLoad()
		{
			return true;
		}

		#endregion


		#region Commands

		public ICommand LoadCommand => new DelegateCommand(Load, CanLoad);
		#endregion

	}
}
