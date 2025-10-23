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
	public class AnswerPollViewModel : Prism.Mvvm.BindableBase
	{
		#region Properties
		public Action CloseAction { get; set; }
// LF3: 6.)
        public Action TimeOverAction { get; set; }
// -------
        Models.Poll _poll;
		public Models.Poll Poll
		{
			get { return _poll; }
			set { _poll = value; OnPropertyChanged(); }
		}

		public string Question1
		{
			get { return Poll?.PollAnswers[0].Answer; }
		}
		public string Question2
		{
			get { return Poll?.PollAnswers[1].Answer; }
		}
		public string Question3
		{
			get { return Poll?.PollAnswers[2].Answer; }
		}
		public string Question4
		{
			get { return Poll?.PollAnswers[3].Answer; }
		}
		public string Question5
		{
			get { return Poll?.PollAnswers[4].Answer; }
		}

		public bool Question1Selected
		{
			get { return (Poll?.SelectedAnswer ?? -1) ==0; }
			set { Poll.SelectedAnswer = 0; OnPropertyChangedRBtn(); }
		}
		public bool Question2Selected
		{
			get { return (Poll?.SelectedAnswer ?? -1) == 1; }
			set { Poll.SelectedAnswer = 1; OnPropertyChangedRBtn(); }
		}
		public bool Question3Selected
		{
			get { return (Poll?.SelectedAnswer ?? -1) == 2; }
			set { Poll.SelectedAnswer = 2; OnPropertyChangedRBtn(); }
		}
		public bool Question4Selected
		{ 
			get { return (Poll?.SelectedAnswer ?? -1) == 3; }
			set { Poll.SelectedAnswer = 3; OnPropertyChangedRBtn(); }
		}
		public bool Question5Selected
		{
			get { return (Poll?.SelectedAnswer ?? -1) == 4; }
			set { Poll.SelectedAnswer = 4; OnPropertyChangedRBtn(); }
		}

		#endregion


		#region Operations

		public async void Load()
		{
			var sp = new Survey.ServiceProxy.ServiceProxy();
			var current = await sp.GetCurrent();

			Poll = current.Convert();

// LF3: 1.)
            Poll.AnswerCount = current.PollVotes.Count;
// --------
            Poll.UserName = Environment.UserName;
			Poll.SelectedAnswer = null;

            OnPropertyChanged(() => Poll);

			OnPropertyChanged(() => Question1);
			OnPropertyChanged(() => Question2);
			OnPropertyChanged(() => Question3);
			OnPropertyChanged(() => Question4);
			OnPropertyChanged(() => Question5);
			OnPropertyChangedRBtn();
		}

		private void OnPropertyChangedRBtn()
		{
			OnPropertyChanged(() => Question1Selected);
			OnPropertyChanged(() => Question2Selected);
			OnPropertyChanged(() => Question3Selected);
			OnPropertyChanged(() => Question4Selected);
			OnPropertyChanged(() => Question5Selected);
		}

		bool CanLoad()
		{
			return true;
		}

		bool _insave = false;
		public async void Save()
		{
			try
			{
				_insave = true;
				OnPropertyChanged(() => Question1Selected);

// LF3: 6.)
	            if (Poll.PollEndTime < DateTime.Now)
                {

                    var sp = new Survey.ServiceProxy.ServiceProxy();
                    await sp.SetAnswer(Poll.PollID, Poll.PollAnswers[Poll.SelectedAnswer.Value].PollAnswerID, Poll.UserName);

                    if (CloseAction != null)
                        CloseAction();
                }
                else
                {
                    TimeOverAction?.Invoke();
                }
// --------
			}
			finally
			{
				_insave = false;
				OnPropertyChanged(() => Question1Selected);
			}
		}

		bool CanSave()
		{
			return !_insave && Poll != null && Poll.SelectedAnswer != null;
		}

		#endregion


		#region Commands

		public ICommand LoadCommand => new DelegateCommand(Load, CanLoad);
		ICommand _saveCommand => new DelegateCommand(Save, CanSave)
			.ObservesProperty(() => Question1Selected)
			.ObservesProperty(() => Question2Selected)
			.ObservesProperty(() => Question3Selected)
			.ObservesProperty(() => Question4Selected)
			.ObservesProperty(() => Question5Selected);
		public ICommand SaveCommand { get { return _saveCommand; } }

		#endregion

	}
}
