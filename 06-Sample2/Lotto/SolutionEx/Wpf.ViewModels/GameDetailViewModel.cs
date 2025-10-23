namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class GameDetailViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public GameDetailViewModel(IUnitOfWork uow)
    {
        _uow = uow;

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

    public RelayCommand CloseCommand { get; set; }

    #endregion

    #region Operations

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