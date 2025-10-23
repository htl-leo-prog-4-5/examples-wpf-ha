using System;
using System.Collections.Generic;
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
	public class AdminPollViewModel : Prism.Mvvm.BindableBase
	{
		#region Properties

		public Action CloseAction { get; set; }
		public Action NewSurveyAction { get; set; }
		public Action ShowPollResultAction { get; set; }
		public Action ShowPollResultSummaryAction { get; set; }

		#endregion

		#region Operations

		private bool CanNewSurvey()
		{
			return true;
		}
		private void NewSurvey()
		{
			if (NewSurveyAction != null)
				NewSurveyAction();
		}
		private bool CanShowPollResult()
		{
			return true;
		}
		private void ShowPollResult()
		{
			if (ShowPollResultAction != null)
				ShowPollResultAction();
		}

		private bool CanShowPollSummaryResult()
		{
			return true;
		}
		private void ShowPollSummaryResult()
		{
			if (ShowPollResultSummaryAction != null)
				ShowPollResultSummaryAction();
		}

		#endregion


		#region Commands

		public ICommand NewSurveyCommand => new DelegateCommand(NewSurvey, CanNewSurvey);
		public ICommand ShowPollResultCommand => new DelegateCommand(ShowPollResult, CanShowPollResult);
		public ICommand ShowPollResultSummaryCommand => new DelegateCommand(ShowPollSummaryResult, CanShowPollSummaryResult);

		#endregion

	}
}
