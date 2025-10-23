using System.Windows;
using NotenschnittMvvmWPF.Views;

namespace NotenschnittMvvmWPF
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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var window = new SchuelerView();
			window.ShowDialog();
		}
	}
}
