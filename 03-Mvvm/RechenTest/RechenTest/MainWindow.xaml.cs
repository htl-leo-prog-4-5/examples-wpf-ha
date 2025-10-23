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

namespace RechenTest
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
        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            SetOperation(Calculate.EOperation.PlusOp);
        }
        private void Button_Click_Minus(object sender, RoutedEventArgs e)
        {
            SetOperation(Calculate.EOperation.MinusOp);
        }
        private void Button_Click_Mul(object sender, RoutedEventArgs e)
        {
            SetOperation(Calculate.EOperation.MulOp);
        }
        private void Button_Click_Div(object sender, RoutedEventArgs e)
        {
            SetOperation(Calculate.EOperation.DivOp);
        }

        void SetOperation(Calculate.EOperation op)
        {
            var vm = DataContext as Calculate;

            if (vm != null)
                vm.Operation = op;
        }
    }
}
