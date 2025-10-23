using GradeCalc.Core;
using GradeCalc.Core.Events;
using GradeCalc.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GradeCalc.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = Dependency.Resolve<MainWindowViewModel>();
            vm.OnError += HandleError;

            DataContext = vm;
        }

        private async void HandleError(object sender, ErrorEventArgs e)
        {
            await this.ShowMessageAsync("Error occurred", e.ErrMsg);
        }
    }
}