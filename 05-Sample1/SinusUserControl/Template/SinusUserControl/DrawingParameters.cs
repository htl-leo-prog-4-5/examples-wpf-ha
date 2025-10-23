using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Windows.Input;

namespace SinusUserControl;

public class DrawingParameters : INotifyPropertyChanged
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

    //TODO This file is used as DataContext
}