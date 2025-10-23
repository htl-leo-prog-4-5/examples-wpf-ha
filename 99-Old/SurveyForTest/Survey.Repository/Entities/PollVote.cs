
using System;
using System.Collections.Generic;

namespace Survey.Repository.Entities
{
	public class PollVote
	{
		public int PollVoteID { get; set; }
		public string UserName { get; set; }

// LF3: 4.)
        public DateTime? VoteTime { get; set; }
// --------

        public virtual Poll Poll { get; set; }

		public int PollAnswerID { get; set; }
		public virtual PollAnswer PollAnswer { get; set; }
	}
}
