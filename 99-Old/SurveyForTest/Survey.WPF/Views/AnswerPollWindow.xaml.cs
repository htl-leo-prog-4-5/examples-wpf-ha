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
	public partial class AnswerPollWindow : Window
	{
		public AnswerPollWindow()
		{
			InitializeComponent();

			var vm = DataContext as AnswerPollViewModel;
			if (vm.CloseAction == null)
				vm.CloseAction = new Action(() => { MessageBox.Show("Stored"); this.Close(); });

// LF3: 6.)
            if (vm.TimeOverAction == null)
                vm.TimeOverAction = new Action(() => { MessageBox.Show("Time over"); });
// -------

        }
    }
}
