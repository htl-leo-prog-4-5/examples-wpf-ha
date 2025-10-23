using TennisMvvmWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TennisMvvmWPF.Helpers;
using System.Windows.Input;
using System.IO;
using System.Collections.ObjectModel;

namespace TennisMvvmWPF.ViewModels;

public class PlayerViewModel : INotifyPropertyChanged
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

    #region Properties


    private Player Player { get; set; } = new Player();

    public string PlayerName
    {
        get { return Player.Name; }
        set { Player.Name = value; OnPropertyChanged(); }
    }
    public int Points
    {
        get { return Player.Points; }
        set { Player.Points = value; OnPropertyChanged(); }
    }

    public ObservableCollection<Results> Results
    {
        get { return Player.Results; }
    }


    string _filenname = @"c:\tmp\TennisErgebnisse.csv";
    public string FileName
    {
        get { return _filenname; }
        set { _filenname = value; OnPropertyChanged(); }
    }

    #endregion

    #region Operations

    public void Load()
    {
        PlayerName = "Ivan Lendl";
        Points = 12345;
        Results.Clear();
        Results.Add(new Results() { City = "Wien", Rank = 1 });
        Results.Add(new Results() { City = "Prag", Rank = 2 });
        Results.Add(new Results() { City = "Paris", Rank = 3 });

        return;
        string alltext = File.ReadAllText(FileName);
        var split = alltext.Split(';');
        PlayerName = split[0];
        Points = int.Parse(split[1]);
    }
    public bool CanLoad()
    {
        return string.IsNullOrEmpty(FileName) == false && File.Exists(FileName);
    }
    public void Save()
    {
        string alltext = $"{PlayerName};{Points}";
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
        return string.IsNullOrEmpty(PlayerName) == false; 
    }

    #endregion

    #region Commands

    public ICommand WinCommand => new DelegateCommand(Win, CanWin);
    public ICommand LoadCommand => new DelegateCommand(Load, CanLoad);
    public ICommand SaveCommand => new DelegateCommand(Save, CanSave);

    #endregion

}