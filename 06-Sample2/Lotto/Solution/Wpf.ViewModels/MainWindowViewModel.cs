namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public MainWindowViewModel(IUnitOfWork uow)
    {
        _uow = uow;

        FilterCommand = new RelayCommand(async () => await FilterAsync(), () => true);
        DetailCommand = new RelayCommand(async () => await DetailAsync(), () => SelectedGame != null);
        DrawCommand   = new RelayCommand(async () => await DrawAsync(),   CanDraw);
        NewCommand    = new RelayCommand(async () => await NewAsync(),    () => true);
    }

    #endregion

    #region Properties/Commands

    private DateTime _dateFrom = new DateTime(DateTime.Today.Year, 1, 1);

    public DateTime DateFrom
    {
        get => _dateFrom;
        set => SetProperty(ref _dateFrom, value);
    }

    private DateTime _dateTo = new DateTime(DateTime.Today.Year, 12, 31);

    public DateTime DateTo
    {
        get => _dateTo;
        set => SetProperty(ref _dateTo, value);
    }

    public ObservableCollection<Game> FilteredGames { get; } = new();

    private Game? _selectedGame;

    public Game? SelectedGame
    {
        get => _selectedGame;
        set
        {
            if (!ReferenceEquals(_selectedGame, value))
            {
                _selectedGame = value;
                OnPropertyChanged();
            }
        }
    }

    public RelayCommand FilterCommand { get; set; }
    public RelayCommand DetailCommand { get; set; }
    public RelayCommand DrawCommand   { get; set; }
    public RelayCommand NewCommand    { get; set; }

    #endregion

    #region Operations

    private async Task FilterAsync()
    {
        await LoadGames(_uow);
    }

    private async Task DetailAsync()
    {
        if (Controller != null && SelectedGame != null)
        {
            await Controller.ShowGameDetailWindowAsync(SelectedGame);
        }

        await LoadGames(_uow);
    }

    private async Task DrawAsync()
    {
        if (Controller != null && SelectedGame != null)
        {
            await Controller.ShowDrawWindowAsync(SelectedGame);
        }

        await LoadGames(_uow);
    }

    private bool CanDraw()
    {
        return Controller != null &&
               SelectedGame != null &&
               SelectedGame.DrawDate is null &&
               SelectedGame.DateTo < DateOnly.FromDateTime(DateTime.Today);
    }

    private async Task NewAsync()
    {
        if (Controller != null)
        {
            await Controller.ShowNewGameWindowAsync();
        }

        await LoadGames(_uow);
    }

    public override async Task InitializeDataAsync()
    {
        await LoadGames(_uow);
    }

    public async Task LoadGames(IUnitOfWork uow)
    {
        var from     = DateOnly.FromDateTime(DateFrom.Date);
        var to       = DateOnly.FromDateTime(DateTo.Date);
        var filtered = await uow.GameRepository.GetNoTrackingAsync(g => g.DateTo >= from && g.DateFrom <= to);

        FilteredGames.Clear();
        foreach (var filteredStation in filtered)
        {
            FilteredGames.Add(filteredStation);
        }
    }

    #endregion
}