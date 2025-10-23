
using System;
using System.Collections.Generic;

namespace Survey.Repository.Entities
{
	public class Poll
	{
		public int PollID { get; set; }
		public string Name { get; set; }

// LF3: 5.)
        public DateTime? PollEndTime { get; set; }
// --------

        public virtual ICollection<PollAnswer> PollAnswers { get; set; }
		public virtual ICollection<PollVote> PollVotes { get; set; }
	}
}
