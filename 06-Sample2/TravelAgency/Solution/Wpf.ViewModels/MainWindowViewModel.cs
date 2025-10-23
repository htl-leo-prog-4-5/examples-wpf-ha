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
        DetailCommand = new RelayCommand(async () => await DetailAsync(), () => SelectedTrip != null);
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

    public ObservableCollection<Trip> Filtered { get; } = new();

    private Trip? _selectedTrip;

    public Trip? SelectedTrip
    {
        get => _selectedTrip;
        set
        {
            if (!ReferenceEquals(_selectedTrip, value))
            {
                _selectedTrip = value;
                OnPropertyChanged();
            }
        }
    }

    public RelayCommand FilterCommand { get; set; }
    public RelayCommand DetailCommand { get; set; }
    public RelayCommand NewCommand    { get; set; }

    #endregion

    #region Operations

    private async Task FilterAsync()
    {
        await LoadTrips(_uow);
    }

    private async Task NewAsync()
    {
        if (Controller != null)
        {
            await Controller.ShowNewTripWindowAsync();
        }

        await LoadTrips(_uow);
    }
    private async Task DetailAsync()
    {
        if (Controller != null && SelectedTrip is not null)
        {
            await Controller.ShowTripWindowAsync(SelectedTrip);
        }

        await LoadTrips(_uow);
    }

    public override async Task InitializeDataAsync()
    {
        await LoadTrips(_uow);
    }

    public async Task LoadTrips(IUnitOfWork uow)
    {
        var from     = DateFrom.Date;
        var to       = DateTo.Date;
        var filtered = await uow.TripRepository
            .GetNoTrackingAsync(g => g.DepartureDateTime >= from && g.ArrivalDateTime <= to, null, nameof(Trip.Route));

        Filtered.Clear();
        foreach (var trip in filtered)
        {
            Filtered.Add(trip);
        }
    }

    #endregion
}