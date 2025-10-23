using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SVCheck.Helpers;

public class BindableBase : INotifyPropertyChanged
{
    #region INPC

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetProperty<T>(ref T dest, T val, [CallerMemberName] string propertyName = null)
        where T : IComparable<T>
    {
        if (dest.CompareTo(val) != 0)
        {
            dest = val;
            RaisePropertyChanged(propertyName);
        }
    }

    #endregion
}