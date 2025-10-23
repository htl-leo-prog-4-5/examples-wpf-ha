using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Logic.Helpers
{
	public static class Converters
	{
		static public DTO.Poll Convert(this Repository.Entities.Poll poll)
		{
			var l = new List<DTO.PollAnswer>();
			var v = new List<DTO.PollVote>();
			foreach (var pa in poll.PollAnswers)
			{
				l.Add(pa.Convert());
			};
			foreach (var pv in poll.PollVotes)
			{
				v.Add(pv.Convert());
			};
			return new DTO.Poll() { PollID = poll.PollID, Name = poll.Name, PollAnswers = l, PollVotes = v};
		}

		static public DTO.PollAnswer Convert(this Repository.Entities.PollAnswer e)
		{
			return new DTO.PollAnswer() { PollAnswerID = e.PollAnswerID, Answer = e.Answer };
		}
		static public DTO.PollVote Convert(this Repository.Entities.PollVote e)
		{
			return new DTO.PollVote() { PollVoteID = e.PollVoteID, UserName = e.UserName, PollAnswerID = e.PollAnswerID };
		}

		static public Repository.Entities.Poll Convert(this DTO.Poll poll)
		{
			var l = new List<Repository.Entities.PollAnswer>();
			var v = new List<Repository.Entities.PollVote>();
			if (poll.PollAnswers != null)
			{
				foreach (var pa in poll.PollAnswers)
				{
					l.Add(pa.Convert());
				};
			}
			if (poll.PollVotes != null)
			{
				foreach (var pv in poll.PollVotes)
				{
					v.Add(pv.Convert());
				};
			}
			return new Repository.Entities.Poll() { PollID = poll.PollID, Name = poll.Name, PollAnswers = l, PollVotes = v };
		}

		static public Repository.Entities.PollAnswer Convert(this DTO.PollAnswer e)
		{
			return new Repository.Entities.PollAnswer() { PollAnswerID = e.PollAnswerID, Answer = e.Answer };
		}
		static public Repository.Entities.PollVote Convert(this DTO.PollVote e)
		{
			return new Repository.Entities.PollVote() { PollVoteID = e.PollVoteID, UserName = e.UserName, PollAnswerID = e.PollAnswerID };
		}

	}
}
