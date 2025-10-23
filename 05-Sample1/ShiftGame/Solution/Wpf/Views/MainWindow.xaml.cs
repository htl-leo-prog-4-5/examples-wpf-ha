namespace Wpf.Views;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Wpf.ViewModels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var vm = (MainWindowViewModel)DataContext;

        for (int row = 0; row < vm.SizeX; row++)
        {
            gameGrid.RowDefinitions.Add(new RowDefinition());
        }

        for (int col = 0; col < vm.SizeY; col++)
        {
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int row = 0; row < vm.SizeX; row++)
        {
            for (int col = 0; col < vm.SizeY; col++)
            {
                var binding = new Binding($"Field[{vm.ToIndex(row, col)}]")
                {
                    Source = vm
                };

                var b = new Button
                {
                    Command  = vm.GetFieldCommand(row, col),
                    FontSize = 50.0
                };
                b.SetBinding(Button.ContentProperty, binding);
                b.SetValue(Grid.RowProperty,    row);
                b.SetValue(Grid.ColumnProperty, col);

                gameGrid.Children.Add(b);
            }
        }
    }
}