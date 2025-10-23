
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Survey.Wpf.Models
{
	public class NewSurvey
	{
		public string Name { get; set; }


// LF3: 5.)
        public DateTime? PollEndTime { get; set; }
// --------

        public string Answer1 { get; set; }
		public string Answer2 { get; set; }
		public string Answer3 { get; set; }
		public string Answer4 { get; set; }
		public string Answer5 { get; set; }
	}
}
