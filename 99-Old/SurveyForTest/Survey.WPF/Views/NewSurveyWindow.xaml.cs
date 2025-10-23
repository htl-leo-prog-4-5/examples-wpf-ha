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
	public partial class NewSurveyWindow : Window
	{
		public NewSurveyWindow()
		{
			InitializeComponent();

			var vm = DataContext as NewSurveyViewModel;
			if (vm.CloseAction == null)
				vm.CloseAction = new Action(() => { MessageBox.Show("Stored"); this.Close(); });
// LF3: 2.)
            if (vm.InvalidSurveyMessageBox == null)
                vm.InvalidSurveyMessageBox = new Action(() => { MessageBox.Show("Invalid survey - missing question"); });
// -------

        }
    }
}
