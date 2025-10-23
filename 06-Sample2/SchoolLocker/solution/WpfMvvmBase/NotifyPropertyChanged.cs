using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace WpfMvvmBase;

public class NotifyPropertyChanged : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
    {
        var memberExpression = (MemberExpression)projection.Body;
        OnPropertyChanged(memberExpression.Member.Name);
    }

    protected void SetProperty<T>(ref T? dest, T? val, [CallerMemberName] string? propertyName = null)
        where T : IComparable<T>
    {
        if (dest?.CompareTo(val) != 0)
        {
            dest = val;
            OnPropertyChanged(propertyName);
        }
    }
}