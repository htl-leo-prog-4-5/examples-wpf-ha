namespace Sudoku.Views;

using System.Collections.Generic;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using Sudoku.Tools;
using Sudoku.ViewModels;

public class WindowNavigator : IWindowNavigator
{
    private readonly Window _window;

    public WindowNavigator(Window window)
    {
        _window = window;
    }

    public ICollection<string> ShowEnterSudoku(ICollection<string> sudoku)
    {
        //TODO: Window Navigator
    }

    public void CloseWindow()
    {
        _window.DialogResult = true;
        _window.Close();
    }
}