namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class TripViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public TripViewModel(IUnitOfWork uow)
    {
        _uow = uow;

        CloseCommand = new RelayCommand(() => Controller!.CloseWindow(), () => true);
    }

    #endregion

    #region Properties/Commands

    private int _tripId = 0;

    public int TripId
    {
        get => _tripId;
        set => SetProperty(ref _tripId, value);
    }

    private Trip _trip = default!;

    public Trip Trip
    {
        get => _trip;
        set => SetProperty(ref _trip, value);
    }

    private Hotel? _selectedHotel;

    public Hotel? SelectedHotel
    {
        get => _selectedHotel;
        set => SetProperty(ref _selectedHotel, value);
    }

    private IList<Hotel> _hotels = new List<Hotel>();

    public IList<Hotel> Hotels
    {
        get => _hotels;
        set => SetProperty(ref _hotels, value);
    }


    public RelayCommand CloseCommand { get; set; }

    #endregion

    #region Operations

    public override async Task InitializeDataAsync()
    {
        await LoadTrips(_uow);
    }

    public async Task LoadTrips(IUnitOfWork uow)
    {
        Trip = (await uow.TripRepository.GetByIdAsync(TripId,
            nameof(Route),
            $"{nameof(Route)}.{nameof(Route.Steps)}",
            $"{nameof(Route)}.{nameof(Route.Steps)}.{nameof(Hotel)}",
            $"{nameof(Route)}.{nameof(Route.Steps)}.{nameof(Ship)}",
            $"{nameof(Route)}.{nameof(Route.Steps)}.{nameof(Plane)}"))!;

    }

    #endregion
}