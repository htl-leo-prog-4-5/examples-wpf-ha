using F1FahrerMvvmWPF.Models;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using F1FahrerMvvmWPF.Helpers;
using System.Windows.Input;
using System.IO;
using System.Windows;

namespace F1FahrerMvvmWPF.ViewModels;

public class F1DriverViewModel : INotifyPropertyChanged
{
    #region INPC 

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
    {
        var memberExpression = (MemberExpression)projection.Body;
        OnPropertyChanged(memberExpression.Member.Name);
    }

    #endregion

    public Func<string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult> MessageBox { get; set; }

    public Func<string, bool, string> BrowseFileNameFunc { get; set; }

    #region Properties


    private F1Driver Driver { get; set; } = new F1Driver();

    public string DriverName
    {
        get => Driver.Name;
        set { Driver.Name = value; OnPropertyChanged(); }
    }
    public int Points
    {
        get => Driver.Points;
        set { Driver.Points = value; OnPropertyChanged(); }
    }

    string _filenname = @"F1Drivers.csv";
    public string FileName
    {
        get => _filenname;
        set { _filenname = value; OnPropertyChanged(); }
    }

    #endregion


    #region Operations

    public void Load()
    {
        string alltext = File.ReadAllText(FileName);
        var split = alltext.Split(';');
        DriverName = split[0];
        Points = int.Parse(split[1]);
    }
    public bool CanLoad()
    {
        return string.IsNullOrEmpty(FileName) == false && File.Exists(FileName);
    }
    public void Save()
    {
        string alltext = $"{DriverName};{Points}";
        File.WriteAllText(FileName, alltext);
    }
    public bool CanSave()
    {
        return string.IsNullOrEmpty(FileName) == false;
    }

    public void Win()
    {
        Points += 15;
    }

    bool CanWin()
    {
        return string.IsNullOrEmpty(DriverName) == false; 
    }

    #endregion

    #region Commands

    public ICommand WinCommand => new DelegateCommand(Win, CanWin);
    public ICommand LoadCommand => new DelegateCommand(Load, CanLoad);
    public ICommand SaveCommand => new DelegateCommand(Save, CanSave);

    #endregion

}