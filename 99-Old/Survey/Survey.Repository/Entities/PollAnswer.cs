
using System.Collections.Generic;

namespace Survey.Repository.Entities
{
	public class PollAnswer
	{
		public int PollAnswerID { get; set; }
		public string Answer { get; set; }

		public virtual Poll Poll { get; set; }
	}
}
