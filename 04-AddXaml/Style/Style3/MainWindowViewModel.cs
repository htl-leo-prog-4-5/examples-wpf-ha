using System.Windows.Input;
using Style3.Tools;

namespace Style3;

public class MainWindowViewModel : BaseViewModel
{
    #region Properties

    private int _currentValue = 1;
    public int CurrentValue
    {
        get => _currentValue;
        set
        {
            SetProperty(ref _currentValue, value);
            OnPropertyChanged(nameof(IsCurrentValuePrim));
        }
    }

    public bool IsCurrentValuePrim
    {
        get => IsPrim(CurrentValue);
    }

    #endregion

    #region Operations

    private bool IsPrim(int value)
    {
        for (int i = 2; i < value;i++)
        {
            if (value % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region Commands

    public ICommand IncrementCommand => new DelegateCommand(() => CurrentValue++);

    #endregion

}