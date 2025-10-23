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

namespace BrowserCustomControl
{
    /// <summary>
    /// Interaction logic for BrowserEditControl.xaml
    /// </summary>
    public partial class BrowserEditControl : UserControl
    {
        public BrowserEditControl()
        {
            InitializeComponent();
        }

        #region Commands

        public static readonly DependencyProperty BrowseCommandProperty = DependencyProperty.Register(
            "BrowseCommand", typeof(ICommand), typeof(BrowserEditControl), new PropertyMetadata(default(ICommand)));

        public ICommand BrowseCommand
        {
            get => (ICommand)GetValue(BrowseCommandProperty);
            set => SetValue(BrowseCommandProperty, value);
        }

        public static DependencyProperty MyContentProperty = DependencyProperty.Register("MyContent", typeof(string), typeof(BrowserEditControl), new PropertyMetadata(""));
        public string MyContent
        {
            get { return (string)GetValue(MyContentProperty); }
            set { SetValue(MyContentProperty, value); }
        }

        #endregion
    }
}
