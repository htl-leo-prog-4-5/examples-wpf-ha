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

		private void _wrap_Click(object sender, RoutedEventArgs e)
		{
			var window = new WrapWindow();
			window.ShowDialog();
		}

		private void _canvas_Click(object sender, RoutedEventArgs e)
		{
			var window = new CanvasWindow();
			window.ShowDialog();
		}

		private void _uniformgrid_Click(object sender, RoutedEventArgs e)
		{
			var window = new UniformGridWindow();
			window.ShowDialog();
		}
		private void _scroll_Click(object sender, RoutedEventArgs e)
		{
			var window = new ScrollWindow2();
			window.ShowDialog();
		}
	}
}
