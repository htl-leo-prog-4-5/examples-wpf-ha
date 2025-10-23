using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.WPF.Models
{
	public class Satz
	{
		public string SpielName { get; set; }

		public string NeuesWort { get; set; }

		public ObservableCollection<Wort> Woerter { get; set; }
	}
}
