using System;
using System.Collections.Generic;
using System.IO;
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

namespace SimpleBindingDataContext
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

		private void Show_Click(object sender, RoutedEventArgs e)
		{
			var person = FindResource("APerson") as Person;
			var file = FindResource("APersonFile") as PersonFile;
			var result = MessageBox.Show($"{person.FirstName} {person.LastName} in Datei {file.FileName} schreiben","Frage",MessageBoxButton.OKCancel);

			if (result == MessageBoxResult.OK)
			{
			    using (var writer = File.AppendText(file.FileName))
			    {
                    writer.WriteLine($"{person.FirstName};{person.LastName}");
			    }
			}
		}
	}
}
