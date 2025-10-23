using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Wpf.Helpers
{
	public static class Converters
	{
		static public DTO.Poll Convert(this Models.Poll poll)
		{
			var l = new List<DTO.PollAnswer>();
			foreach (var pa in poll.PollAnswers)
			{
				l.Add(Convert(pa));
			};
			return new DTO.Poll() { PollID = poll.PollID, Name = poll.Name, PollAnswers = l };
		}
		static public DTO.PollAnswer Convert(Models.PollAnswer e)
		{
			return new DTO.PollAnswer() { PollAnswerID = e.PollAnswerID, Answer = e.Answer };
		}

		static public Models.Poll Convert(this DTO.Poll poll)
		{
			var l = new ObservableCollection<Models.PollAnswer>();
			foreach (var pa in poll.PollAnswers)
			{
				l.Add(Convert(pa));
			};
			return new Models.Poll() { PollID = poll.PollID, Name = poll.Name, PollAnswers = l };
		}
		static public Models.PollAnswer Convert(DTO.PollAnswer e)
		{
			return new Models.PollAnswer() { PollAnswerID = e.PollAnswerID, Answer = e.Answer };
		}

	}
}
