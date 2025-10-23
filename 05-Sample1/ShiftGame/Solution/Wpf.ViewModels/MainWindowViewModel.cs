namespace Wpf.ViewModels;

using System.Threading.Tasks;
using System.Windows.Input;

using Base.WpfMvvm;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public MainWindowViewModel()
    {
        InitField(4, 4);
    }

    #endregion

    #region Properties/Commands

    public ICommand NewGameCommand => new RelayCommand(NewGame);

    private int _moveCount = 0;

    public int MoveCount
    {
        get => _moveCount;
        set => SetProperty(ref _moveCount, value);
    }

    public ICommand GetFieldCommand(int row, int col)
    {
        return new RelayCommand(() => { MoveField(row, col); }, () => CanMoveField(row, col));
    }

    public string[] Field { get; private set; } = null!; // we can only bind single dim array to control 
    public int      SizeX { get; private set; }
    public int      SizeY { get; private set; }

    #endregion

    #region Operations

    public int ToIndex(int row, int col) => row * SizeY + col;

    protected void InitField(int x, int y)
    {
        SizeX = x;
        SizeY = y;
        Field = new string[SizeX * SizeY];
        NewGame();
    }

    private bool IsSorted()
    {
        for (int x = 0; x < SizeX; x++)
        for (int y = 0; y < SizeY; y++)
            if (GetFieldContent(x, y) != (1 + ToIndex(x, y)))
                return x == (SizeX - 1) && y == (SizeY - 1);
        return true;
    }

    private bool TrySetField(int index, int val)
    {
        int x = index / SizeY;
        int y = index % SizeY;

        if (!IsFieldEmpty(x, y)) return false;

        SetField(x, y, val.ToString());
        return true;
    }

    private void Swap(int x1, int y1, int x2, int y2)
    {
        string tmp = GetField(x1,     y1);
        SetField(x1, y1, GetField(x2, y2));
        SetField(x2, y2, tmp);
        MoveCount++;
    }

    private void SetField(int x, int y, string val)
    {
        Field[ToIndex(x, y)] = val;
    }

    private string GetField(int x, int y)
    {
        return Field[ToIndex(x, y)];
    }

    protected void MoveField(int x, int y)
    {
        if (x < (SizeX - 1) && IsFieldEmpty(x + 1, y))
        {
            Swap(x, y, x + 1, y);
        }
        else if (x > 0 && IsFieldEmpty(x - 1, y))
        {
            Swap(x, y, x - 1, y);
        }
        else if (y < (SizeY - 1) && IsFieldEmpty(x, y + 1))
        {
            Swap(x, y, x, y + 1);
        }
        else if (y > 0 && IsFieldEmpty(x, y - 1))
        {
            Swap(x, y, x, y - 1);
        }

        OnPropertyChanged("Field");
    }

    private int GetFieldContent(int x, int y)
    {
        return IsFieldEmpty(x, y) ? -1 : int.Parse(GetField(x, y));
    }

    private bool IsFieldEmpty(int x, int y)
    {
        return string.IsNullOrEmpty(GetField(x, y));
    }

    protected bool CanMoveField(int x, int y)
    {
        if (IsFieldEmpty(x, y)) return false;

        if (IsSorted()) return false;

        if (x < (SizeX - 1) && IsFieldEmpty(x + 1, y)) return true;

        if (x > 0 && IsFieldEmpty(x - 1, y)) return true;

        if (y < (SizeY - 1) && IsFieldEmpty(x, y + 1)) return true;

        if (y > 0 && IsFieldEmpty(x, y - 1)) return true;

        return false;
    }

    private void NewGame()
    {
        for (int x = 0; x < SizeX; x++)
        for (int y = 0; y < SizeY; y++)
            SetField(x, y, null);

        var rnd = new Random();

        for (int i = 1; i <= (SizeX * SizeY) - 1; i++)
        {
            while (!TrySetField(rnd.Next(0, SizeX * SizeY), i))
            {
            }
        }

        MoveCount = 0;
        OnPropertyChanged("Field");
    }

    public async override Task InitializeDataAsync()
    {
        await Task.CompletedTask;
    }

    #endregion
}