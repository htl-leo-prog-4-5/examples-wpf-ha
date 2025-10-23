
using System;
using System.Collections.Generic;

namespace Survey.DTO
{
	public class Poll
	{
		public int PollID { get; set; }
		public string Name { get; set; }
        public DateTime? PollEndTime { get; set; }

        public virtual ICollection<PollAnswer> PollAnswers { get; set; }
		public virtual ICollection<PollVote> PollVotes { get; set; }
	}
}
