using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.DTO;
using Survey.Repository;
using Survey.Logic.Helpers;

namespace Survey.Logic
{
	public class SurveyController
	{
		public async Task<IEnumerable<Poll>> GetAll()
		{
			var rep = new PollRepository();
			var polls = await rep.GetPolls();

			var ret = new List<Poll>();
			foreach (var p in polls)
			{
				ret.Add(p.Convert());
			};
			return ret;
		}

		public async Task<bool> Answer(int pollid, int answerid, string username)
		{
			var rep = new PollRepository();
			var poll = await rep.GetPoll(pollid);

			if (poll != null && poll.PollAnswers != null && poll.PollAnswers.FirstOrDefault((a) => a.PollAnswerID == answerid) != null)
			{
				var answer = poll.PollAnswers.FirstOrDefault((a) => a.PollAnswerID == answerid);

				if (answer != null)
				{
					rep.AddVote(new Repository.Entities.PollVote() { Poll = poll, PollAnswerID = answerid, UserName = username });
					var result = await rep.MySaveChangesAsync();
					if (result > 0)
						return true;
				}
			}

			return false;
		}

		public async Task<int> GetCurrentPollID()
		{
			var rep = new PollRepository();
			return await rep.GetMaxPollID();
		}

		public async Task<bool> AddPoll(DTO.Poll poll)
		{
			var rep = new PollRepository();

			var pollentity = poll.Convert();

			await rep.AddNewPoll(pollentity);

			return true;
		}

		public async Task<IEnumerable<PollResult>> GetResult(int pollid)
		{
			var rep = new PollRepository();
			var poll = await rep.GetPoll(pollid);

			return poll.PollVotes.Select((v) => new PollResult() {
							UserName = v.UserName,
							Answer = v.Poll.PollAnswers.First((a) => a.PollAnswerID == v.PollAnswerID).Answer } );
		}
		public async Task<IEnumerable<PollResultSummary>> GetResultSummary(int pollid)
		{
			var rep = new PollRepository();
			var poll = await rep.GetPoll(pollid);

			return poll.PollVotes.GroupBy((g)=>g.PollAnswerID).Select((v) => new PollResultSummary()
			{
				Count = v.Count(),
				Answer = poll.PollAnswers.First((a) => a.PollAnswerID == v.Key).Answer
			});
		}
	}
}
