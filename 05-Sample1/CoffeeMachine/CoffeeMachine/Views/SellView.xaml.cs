using CoffeeMachine.ViewModels;
using System.IO;
using System.Windows;

namespace CoffeeMachine.Views
{
    public partial class SellView : Window
    {
        public SellView()
        {
            InitializeComponent();

            var vm = (SellViewModel) DataContext;

            vm.CloseAction = () => { Close(); };

            vm.MsgBox = (msg) =>
            {
                MessageBox.Show(msg);
                return true;
            };
        }
    }
}
