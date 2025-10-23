namespace Sudoku.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Solve.Abstraction;

using Sudoku.Models;
using Sudoku.Tools;

public class MainWindowViewModel : BaseViewModel
{
    #region crt

    public MainWindowViewModel()
    {
        //TODO: we need a ISudokuSolveService solveService => DI

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                _cell[row, col] = new Cell()
                {
                    Row = row,
                    Col = col
                };
            }
        }
    }

    #endregion

    #region Properties

    private Cell[,] _cell = new Cell[9, 9];

    public Cell GetCell(int row, int col) => _cell[row, col];

    #endregion

    #region Commands

    //public ICommand CellCommand   => new RelayCommand(async (parameter) => await NextValue(parameter), CanCellCommand);

    #endregion

    #region Operations

    //TODO You need a public async Task LoadDataAsync()

    #endregion

    #region Helpers


    #endregion
}