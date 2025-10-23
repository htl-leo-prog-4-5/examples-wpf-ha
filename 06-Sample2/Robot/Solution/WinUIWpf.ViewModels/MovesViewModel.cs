namespace WinUIWpf.ViewModels;

using System;

using Core.Contracts;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Entities;
using Core.QueryResults;

public class MovesViewModel : BaseViewModel
{
    #region ctr

    public MovesViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    private IUnitOfWork _uow;

    #endregion

    #region Properties

    public Race Race { get; set; } = default!;

    public IWindowNavigator? Controller { get; set; }

    public ObservableCollection<Move> Moves { get; set; } = new ObservableCollection<Move>();

    private Move? _selectedMove;

    public Move? SelectedMove
    {
        get => _selectedMove;
        set
        {
            if (SetProperty(ref _selectedMove, value))
            {
                IsEditMode = false;
                if (_selectedMove == null)
                {
                    Direction = null;
                    Speed     = null;
                    Duration  = null;
                }
                else
                {
                    Direction = _selectedMove.Direction;
                    Speed     = _selectedMove.Speed;
                    Duration  = _selectedMove.Duration;
                }
            }
        }
    }

    private bool _isEditMode;

    public bool IsEditMode
    {
        get => _isEditMode;
        set
        {
            if (SetProperty(ref _isEditMode, value))
            {
                OnPropertyChanged(nameof(IsNotEditMode));
            }
        }
    }

    public bool IsNotEditMode
    {
        get => !IsEditMode;
    }

    private int? _direction;

    public int? Direction
    {
        get => _direction;
        set => SetProperty(ref _direction, value);
    }

    private int? _speed;

    public int? Speed
    {
        get => _speed;
        set => SetProperty(ref _speed, value);
    }

    private int? _duration;

    public int? Duration
    {
        get => _duration;
        set => SetProperty(ref _duration, value);
    }

    public RelayCommand EditMoveCommand   => new RelayCommand(EditMove,   () => SelectedMove != null && IsNotEditMode);
    public RelayCommand UpdateMoveCommand => new RelayCommand(async () => await UpdateMove(), () => IsEditMode);

    #endregion

    #region Operations

    public async Task LoadDataAsync()
    {
        Moves.Clear();
        var moves = await _uow.Move.GetNoTrackingAsync(r => r.RaceId == Race.Id);
        foreach (var v in moves)
        {
            Moves.Add(v);
        }
    }

    private void EditMove()
    {
        IsEditMode = true;
    }

    private async Task UpdateMove()
    {
        var msg = string.Empty;

        if (Direction != SelectedMove!.Direction)
        {
            msg += $"Direction {SelectedMove.Direction}=>{Direction} ";
        }

        if (Speed != SelectedMove!.Speed)
        {
            msg += $"Speed {SelectedMove.Speed}=>{Speed} ";
        }

        if (Duration != SelectedMove!.Duration)
        {
            msg += $"Duration {SelectedMove.Duration}=>{Duration} ";
        }

        if (!string.IsNullOrEmpty(msg) && (Controller?.AskYesNoMessageBox("Question", $"Update: {msg}") ?? false))
        {
            var raceInDb = (await _uow.Move.GetByIdAsync(SelectedMove.Id)) ?? throw new ArgumentNullException();
            raceInDb.Direction = Direction ?? 0;
            raceInDb.Duration  = Duration ?? 0;
            raceInDb.Speed     = Speed ?? 0;
            await _uow.SaveChangesAsync();
            await LoadDataAsync();
        }
        else
        {
            SelectedMove = null;
        }

        IsEditMode = false;
    }

    /*

    private async Task DeleteRace()
    {
        if (SelectedMove != null && (Controller?.AskYesNoMessageBox("Question", $"Delete race: Driver: {SelectedMove.Driver!.Name}, Car: {SelectedMove.Car!.Name}?") ?? false))
        {
            var race = await _uow.Race.GetByIdAsync(SelectedMove.Id);
            _uow.Race.Remove(race!);
            await _uow.SaveChangesAsync();
            Moves.Remove(SelectedMove);
        }
    }
*/

    #endregion
}