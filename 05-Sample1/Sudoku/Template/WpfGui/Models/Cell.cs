namespace Sudoku.Models;

using Sudoku.Tools;

public class Cell : NotifyPropertyChanged
{
    #region Sudoku Properties

	//TODO Add properties for cell 

    public string? Text => No ?? Possible;

    private string? _no = null;

    public string? No
    {
        get => _no;
        set
        {
            _possible = null;
            SetProperty(ref _no, value);
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(IsAssigned));
            OnPropertyChanged(nameof(IsPossibleAssigned));
            OnPropertyChanged(nameof(Possible));
        }
    }

    private string? _possible = null;

    public string? Possible
    {
        get => _possible;
        set
        {
            _no = null;
            SetProperty(ref _possible, value);
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(IsAssigned));
            OnPropertyChanged(nameof(IsPossibleAssigned));
            OnPropertyChanged(nameof(No));
        }
    }

    #endregion

    #region Row/Col Property

    private int _row;

    public int Row
    {
        get => _row;
        set => SetProperty(ref _row, value);
    }

    private int _col;

    public int Col
    {
        get => _col;
        set => SetProperty(ref _col, value);
    }

    #endregion
}