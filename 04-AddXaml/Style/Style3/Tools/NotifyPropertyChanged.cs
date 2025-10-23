using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Style3.Tools;

public class NotifyPropertyChanged : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetProperty<T>(ref T dest, T val, [CallerMemberName] string propertyName = null)
        where T : IComparable<T>
    {
        if (dest.CompareTo(val) != 0)
        {
            dest = val;
            OnPropertyChanged(propertyName);
        }
    }
}