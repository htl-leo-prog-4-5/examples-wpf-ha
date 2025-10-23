using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TickTackToe;

public class TickTackToeGame : INotifyPropertyChanged
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

    int _moveCount = 0;
    public int MoveCount
    {
        get
        {
            return _moveCount;
        }
        private set
        {
            _moveCount = value;
            OnPropertyChanged();
        }
    }

    string _winner;
    public string Winner
    {
        get { return _winner; }
        private set
        {
            _winner = value;
            OnPropertyChanged();
        }
    }

    public string NextPlayer
    {
        get { return string.IsNullOrEmpty(Winner) ? ((MoveCount % 2) ==0 ? "O" : "X") : "" ; }
    }

    string _field00;
    public string Field00
    {
        get
        {
            return _field00;
        }
        private set
        {
            _field00 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }

    string _field01;

    public string Field01
    {
        get
        {
            return _field01;
        }
        private set
        {
            _field01 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }
    string _field02;

    public string Field02
    {
        get
        {
            return _field02;
        }
        private set
        {
            _field02 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }

    string _field10;
    public string Field10
    {
        get
        {
            return _field10;
        }
        private set
        {
            _field10 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }

    string _field11;

    public string Field11
    {
        get
        {
            return _field11;
        }
        private set
        {
            _field11 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }
    string _field12;

    public string Field12
    {
        get
        {
            return _field12;
        }
        private set
        {
            _field12 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }

    string _field20;
    public string Field20
    {
        get
        {
            return _field20;
        }
        private set
        {
            _field20 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }

    string _field21;

    public string Field21
    {
        get
        {
            return _field21;
        }
        private set
        {
            _field21 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }
    string _field22;

    public string Field22
    {
        get
        {
            return _field22;
        }
        private set
        {
            _field22 = value;
            OnPropertyChanged();
            CheckForWinner();
        }
    }

    #endregion

    #region Operations

    void CheckForWinner()
    {
        if (!string.IsNullOrEmpty(Field00) && Field00 == Field01 && Field00 == Field02)
        {
            Winner = Field00;
        }
        if (!string.IsNullOrEmpty(Field10) && Field10 == Field11 && Field10 == Field12)
        {
            Winner = Field10;
        }
        if (!string.IsNullOrEmpty(Field20) && Field20 == Field21 && Field20 == Field22)
        {
            Winner = Field10;
        }

        if (!string.IsNullOrEmpty(Field00) && Field00 == Field10 && Field00 == Field20)
        {
            Winner = Field00;
        }
        if (!string.IsNullOrEmpty(Field01) && Field01 == Field11 && Field01 == Field21)
        {
            Winner = Field01;
        }
        if (!string.IsNullOrEmpty(Field02) && Field02 == Field12 && Field02 == Field22)
        {
            Winner = Field02;
        }

        if (!string.IsNullOrEmpty(Field00) && Field00 == Field11 && Field00 == Field22)
        {
            Winner = Field00;
        }
        if (!string.IsNullOrEmpty(Field02) && Field02 == Field11 && Field02 == Field20)
        {
            Winner = Field02;
        }
    }

    string GetNextBtnText()
    {
        MoveCount++;
        OnPropertyChanged(() => NextPlayer);
        OnPropertyChanged(() => MoveCount);
        return ((MoveCount) % 2) == 0 ? "X" : "O";
    }


    private void NewGame()
    {
        Field00 = Field01 = Field02 = null;
        Field10 = Field11 = Field12 = null;
        Field20 = Field21 = Field22 = null;
        Winner = null;
        MoveCount = 0;
        OnPropertyChanged(() => NextPlayer);
    }

    #endregion

    #region Commands

    public ICommand SetField00Command { get { return new DelegateCommand(() => { Field00 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field00)); } }
    public ICommand SetField01Command { get { return new DelegateCommand(() => { Field01 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field01)); } }
    public ICommand SetField02Command { get { return new DelegateCommand(() => { Field02 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field02)); } }

    public ICommand SetField10Command { get { return new DelegateCommand(() => { Field10 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field10)); } }
    public ICommand SetField11Command { get { return new DelegateCommand(() => { Field11 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field11)); } }
    public ICommand SetField12Command { get { return new DelegateCommand(() => { Field12 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field12)); } }

    public ICommand SetField20Command { get { return new DelegateCommand(() => { Field20 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field20)); } }
    public ICommand SetField21Command { get { return new DelegateCommand(() => { Field21 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field21)); } }
    public ICommand SetField22Command { get { return new DelegateCommand(() => { Field22 = GetNextBtnText(); }, () => string.IsNullOrEmpty(Winner) && string.IsNullOrEmpty(Field22)); } }

    public ICommand NewGameCommand		{ get { return new DelegateCommand(() => { NewGame();}); } }

    #endregion
}