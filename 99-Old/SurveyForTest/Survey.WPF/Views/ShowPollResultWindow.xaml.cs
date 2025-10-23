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
using System.Windows.Shapes;
using Survey.Wpf.ViewModels;

namespace Survey.Wpf.Views
{
	/// <summary>
	/// Interaction logic for AnswerPollWindow.xaml
	/// </summary>
	public partial class ShowPollResultWindow : Window
	{
		public ShowPollResultWindow()
		{
			InitializeComponent();

			var vm = DataContext as ShowPollResultViewModel;
			if (vm.CloseAction == null)
				vm.CloseAction = new Action(() => { this.Close(); });

		}
	}
}
