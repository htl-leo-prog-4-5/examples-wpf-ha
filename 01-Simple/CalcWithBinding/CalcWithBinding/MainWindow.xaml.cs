using System.Windows;

namespace CalcWithBinding
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

		public Calculator Calc { get { return FindResource("ACalculator") as Calculator; } }

		private void _1_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(1);
		}

		private void _2_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(2);
		}

		private void _4_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(4);
		}

		private void _3_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(3);
		}

		private void _5_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(5);
		}

		private void _6_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(6);
		}

		private void _7_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(7);
		}

		private void _8_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(8);
		}

		private void _9_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(9);
		}

		private void _0_Click(object sender, RoutedEventArgs e)
		{
			Calc.AddDigit(0);
		}

		private void Enter_Click(object sender, RoutedEventArgs e)
		{
			Calc.Enter();
		}

		private void Plus_Click(object sender, RoutedEventArgs e)
		{
			Calc.Plus();
		}

		private void Minus_Click(object sender, RoutedEventArgs e)
		{
			Calc.Minus();

		}

		private void Mul_Click(object sender, RoutedEventArgs e)
		{
			Calc.Mul();

		}

		private void Div_Click(object sender, RoutedEventArgs e)
		{
			Calc.Div();

		}
	}
}
