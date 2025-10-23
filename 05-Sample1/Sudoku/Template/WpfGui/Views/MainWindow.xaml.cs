namespace Sudoku.Views;

using System.Windows;
using System.Windows.Controls;

using Sudoku.Controls;
using Sudoku.ViewModels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel vm)
    {
        vm.Controller = new WindowNavigator(this);

        InitializeComponent();

        DataContext = vm;

        //TODO Dynamic create controls!
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                //TODO
                SudokuGrid.Children.Add(c);
            }
        }

        //TODO you also can create the column and row header (label) control here

        Loaded += async (v, e) =>
        {
            await (DataContext as MainWindowViewModel)!
                .LoadDataAsync();
        };
    }
}