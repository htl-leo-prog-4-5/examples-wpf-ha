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

namespace LayoutSample
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

		private void _stack_Click(object sender, RoutedEventArgs e)
		{
			var window = new StackPanelWindow();
			window.ShowDialog();
		}

		private void _grid_Click(object sender, RoutedEventArgs e)
		{
			var window = new GridWindow();
			window.ShowDialog();
		}
	}
}
