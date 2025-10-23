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
			var stackpanelwindow = new StackPanelWindow();
			stackpanelwindow.ShowDialog();
		}

		private void _dock_Click(object sender, RoutedEventArgs e)
		{
			var dockpanelWindow = new DockWindow();
			dockpanelWindow.ShowDialog();
		}

		private void _grid_Click(object sender, RoutedEventArgs e)
		{
			var gridWindow = new GridWindow();
			gridWindow.ShowDialog();
		}
		private void _gridSplitter_Click(object sender, RoutedEventArgs e)
		{
			var gridSplitterWindow = new GridSplitterWindow();
			gridSplitterWindow.ShowDialog();
		}
	}
}
