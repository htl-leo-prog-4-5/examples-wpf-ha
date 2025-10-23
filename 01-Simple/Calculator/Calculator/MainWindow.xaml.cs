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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			Clear();
		}

        delegate int Calculation(int x, int y);
        Calculation _calc = null;
		bool _nextEmpty = true;

        private int Value 
        {
            get { string content = _result.Content.ToString(); return string.IsNullOrEmpty(content) ? 0 : int.Parse(content); }
            set { _result.Content = value.ToString();  } 
        }

        private int LastValue { get; set; }

        private void AddDigit(string digit)
        {
			if (_nextEmpty)
			{
				_result.Content = digit;
				_nextEmpty = false;
			}
			else
				_result.Content = int.Parse(_result.Content.ToString() + digit).ToString();
        }

        private void _1_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("1");
        }

        private void _2_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("2");
        }

        private void _4_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("4");
        }

        private void _3_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("3");
        }

        private void _5_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("5");
        }

        private void _6_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("6");
        }

        private void _7_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("7");
        }

        private void _8_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("8");
        }

        private void _9_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("9");
        }

        private void _0_Click(object sender, RoutedEventArgs e)
        {
            AddDigit("0");
        }

        void SetOp(Calculation calc)
        {
			Calc();
			LastValue = Value;
			_nextEmpty = true;
            _calc = calc;
		}

		private void Calc()
		{
			if (_calc != null)
			{
				Value = _calc(LastValue, Value);
				_calc = null;
			}
			_nextEmpty = true;
		}

		private void _plus_Click(object sender, RoutedEventArgs e)
        {
            SetOp((int x, int y) => x+y);
        }

        private void _minus_Click(object sender, RoutedEventArgs e)
        {
			SetOp((int x, int y) => x - y);
		}

		private void _mul_Click(object sender, RoutedEventArgs e)
        {
			SetOp((int x, int y) => x * y);
		}

		private void _div_Click(object sender, RoutedEventArgs e)
        {
			SetOp((int x, int y) => x / y);
		}

		private void _eq_Click(object sender, RoutedEventArgs e)
        {
			Calc();
        }
        private void _C_Click(object sender, RoutedEventArgs e)
		{
			Clear();
		}

		private void Clear()
		{
			LastValue = 0;
			_result.Content = "";
			_nextEmpty = true;
			_calc = null;
		}
	}
}
