
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Survey.Wpf.Models
{
	public class Poll
	{
		public int PollID { get; set; }
		public string Name { get; set; }
// LF3: 5.)
        public DateTime? PollEndTime { get; set; }
// --------

        public string UserName { get; set; }
		public int? SelectedAnswer { get; set; }
		public virtual ObservableCollection<PollAnswer> PollAnswers { get; set; }


// LF3: 1.)
        public int AnswerCount { get; set; }
// --------
    }
}
