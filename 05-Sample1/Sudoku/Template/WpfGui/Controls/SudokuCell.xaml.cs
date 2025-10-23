namespace Sudoku.Controls;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Sudoku.Models;

public partial class SudokuCell : UserControl
{
    #region ctr

    public SudokuCell()
    {
        InitializeComponent();
    }

    #endregion

    #region CommandProperty

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SudokuCell), new PropertyMetadata(default(ICommand)));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object),
        typeof(SudokuCell), new PropertyMetadata("1", OnCommandParameterChanged));

    #endregion

    #region CommandParameterProperty

    public object CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    private static void OnCommandParameterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (SudokuCell)dependencyObject;
        ctrl.InvalidateVisual();
    }
    #endregion
}