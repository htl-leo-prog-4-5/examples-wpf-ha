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
	public class NewSurveyViewModel : Prism.Mvvm.BindableBase
	{
		#region Properties
		public Action CloseAction { get; set; }

		Models.NewSurvey _newSurvey = new Models.NewSurvey();
		public Models.NewSurvey NewSurvey
		{
			get { return _newSurvey; }
			set { _newSurvey = value; OnPropertyChanged(); }
		}

		#endregion


		#region Operations

		bool _insave = false;
		public async void Save()
		{
			try
			{
				_insave = true;
				OnPropertyChanged(() => NewSurvey);

				var sp = new Survey.ServiceProxy.ServiceProxy();

				DTO.Poll poll = new DTO.Poll()
				{
					Name = _newSurvey.Name,
					PollAnswers = new List<DTO.PollAnswer>()					
				} ;
				poll.PollAnswers.Add(new DTO.PollAnswer() { Answer = _newSurvey.Answer1 });
				poll.PollAnswers.Add(new DTO.PollAnswer() { Answer = _newSurvey.Answer2 });
				poll.PollAnswers.Add(new DTO.PollAnswer() { Answer = _newSurvey.Answer3 });
				poll.PollAnswers.Add(new DTO.PollAnswer() { Answer = _newSurvey.Answer4 });
				poll.PollAnswers.Add(new DTO.PollAnswer() { Answer = _newSurvey.Answer5 });

				await sp.AddNewSurvey(poll);

				if (CloseAction != null)
					CloseAction();
			}
			finally
			{
				_insave = false;
				OnPropertyChanged(() => NewSurvey);
			}
		}

		bool CanSave()
		{
			return !_insave;
		}

		#endregion


		#region Commands

		ICommand _saveCommand => new DelegateCommand(Save, CanSave);
		public ICommand SaveCommand { get { return _saveCommand; } }

		#endregion

	}
}
