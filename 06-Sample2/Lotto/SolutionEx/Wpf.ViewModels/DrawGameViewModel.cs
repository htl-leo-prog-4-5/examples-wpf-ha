namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core;
using Core.Contracts;
using Core.Entities;

public class DrawGameViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    private const uint MaxValue = 45;

    public DrawGameViewModel(IUnitOfWork uow)
    {
        _uow = uow;

        DrawCommand  = new RelayCommand(async () => await DrawAsync(),   CanDraw);
        CloseCommand = new RelayCommand(() => Controller?.CloseWindow(), () => true);
    }

    #endregion

    #region Properties/Commands

    public int GameId { get; set; }

    private Game? _game;

    public Game? Game
    {
        get => _game;
        set
        {
            if (!ReferenceEquals(_game, value))
            {
                _game = value;
                OnPropertyChanged();
            }
        }
    }

    private uint? _no1;

    public uint? No1
    {
        get => _no1;
        set => SetProperty(ref _no1, value);
    }

    private uint? _no2;

    public uint? No2
    {
        get => _no2;
        set => SetProperty(ref _no2, value);
    }

    private uint? _no3;

    public uint? No3
    {
        get => _no3;
        set => SetProperty(ref _no3, value);
    }

    private uint? _no4;

    public uint? No4
    {
        get => _no4;
        set => SetProperty(ref _no4, value);
    }

    private uint? _no5;

    public uint? No5
    {
        get => _no5;
        set => SetProperty(ref _no5, value);
    }

    private uint? _no6;

    public uint? No6
    {
        get => _no6;
        set => SetProperty(ref _no6, value);
    }

    private uint? _noZZ;

    public uint? NoZZ
    {
        get => _noZZ;
        set => SetProperty(ref _noZZ, value);
    }


    public RelayCommand DrawCommand  { get; set; }
    public RelayCommand CloseCommand { get; set; }

    #endregion

    #region Operations

    private async Task DrawAsync()
    {
        var normalized = new byte[] { ToByte(No1), ToByte(No2), ToByte(No3), ToByte(No4), ToByte(No5), ToByte(No6) }.Normalize().Distinct().ToArray();

        Game.No1 = normalized[0];
        Game.No2 = normalized[1];
        Game.No3 = normalized[2];
        Game.No4 = normalized[3];
        Game.No5 = normalized[4];
        Game.No6 = normalized[5];
        Game.NoX = ToByte(NoZZ);

        Game.DrawDate = DateTime.Now;

        await _uow.SaveChangesAsync();

        Controller!.CloseWindow();
    }

    bool IsValidNo(uint? no)
    {
        return no.HasValue && no.Value >= 1 && no.Value <= MaxValue;
    }

    byte ToByte(uint? no)
    {
        return (byte)(no ?? 1);
    }

    private bool CanDraw()
    {
        if (!IsValidNo(No1) || !IsValidNo(No2) || !IsValidNo(No3) || !IsValidNo(No4) || !IsValidNo(No5) || !IsValidNo(No6) || !IsValidNo(NoZZ))
        {
            return false;
        }

        var normalized   = new byte[] { ToByte(No1), ToByte(No2), ToByte(No3), ToByte(No4), ToByte(No5), ToByte(No6) }.Normalize().Distinct().ToArray();
        var normalizedZZ = new byte[] { ToByte(No1), ToByte(No2), ToByte(No3), ToByte(No4), ToByte(No5), ToByte(No6), ToByte(NoZZ) }.Normalize().Distinct().ToArray();

        return normalized.Length == 6 && normalizedZZ.Length == 7;
    }

    public override async Task InitializeDataAsync()
    {
        await LoadGame(_uow);
    }

    public async Task LoadGame(IUnitOfWork uow)
    {
        Game = await uow.GameRepository.GetByIdAsync(GameId);
    }

    #endregion
}