
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Survey.Wpf.Models
{
	public class Poll
	{
		public int PollID { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }
		public int? SelectedAnswer { get; set; }
		public virtual ObservableCollection<PollAnswer> PollAnswers { get; set; }
	}
}
