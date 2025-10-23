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
	/// Interaction logic for AdminPollWindow.xaml
	/// </summary>
	public partial class AdminPollWindow : Window
	{
		public AdminPollWindow()
		{
			InitializeComponent();

			var vm = DataContext as AdminPollViewModel;
			if (vm.CloseAction == null)
				vm.CloseAction = new Action(() => { this.Close(); });

			if (vm.NewSurveyAction == null)
				vm.NewSurveyAction = new Action(() => 
				{
					var dlg = new NewSurveyWindow();
					dlg.ShowDialog();
				});

			if (vm.ShowPollResultAction == null)
				vm.ShowPollResultAction = new Action(() =>
				{
					var dlg = new ShowPollResultWindow();
					dlg.ShowDialog();
				});

			if (vm.ShowPollResultSummaryAction == null)
				vm.ShowPollResultSummaryAction = new Action(() =>
				{
					var dlg = new ShowPollResultSummaryWindow();
					dlg.ShowDialog();
				});

		}
	}
}
