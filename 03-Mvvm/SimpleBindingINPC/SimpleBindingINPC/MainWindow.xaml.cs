using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleBindingINPC
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void _forUSA_Click(object sender, RoutedEventArgs e)
		{
			var p = FindResource("APerson") as PersonAddress;
			p.Grade = "Dr.";
			p.FirstName = "Bill";
			p.LastName = "Gates";
			p.City = "Redmond";
			p.Country = "USA";
			p.ZipCode = "WA 98052-6399";
			p.Street = "One Microsoft Way";
		}

		private void _forUK_Click(object sender, RoutedEventArgs e)
		{
			var p = FindResource("APerson") as PersonAddress;
			p.Grade = "Dr.";
			p.FirstName = "Bill";
			p.LastName = "Gates";
			p.City = "Berkshire";
			p.Country = "Uk";
			p.ZipCode = "RG6 1WG";
			p.Street = "Thames Valley Park Reading";
		}

		private void _forAUT_Click(object sender, RoutedEventArgs e)
		{
			var p = FindResource("APerson") as PersonAddress;
			p.Grade = "Dr.";
			p.FirstName = "Bill";
			p.LastName = "Gates";
			p.City = "Wien";
			p.Country = "AUT";
			p.ZipCode = "1120 ";
			p.Street = "Am Euro Platz 3/Eingang B";
		}
	}
}
