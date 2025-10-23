namespace Wpf.ViewModels;

using System.Collections.ObjectModel;
using System.Linq.Expressions;

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
        DetailCommand = new RelayCommand(async () => await DetailAsync(), () => SelectedStation != null);
    }

    #endregion

    #region Properties/Commands

    public ObservableCollection<Station> FilteredStations { get; } = new();

    private Station? _selectedStation;

    public Station? SelectedStation
    {
        get => _selectedStation;
        set => SetProperty(ref _selectedStation, value);
    }

    public IList<City> Cities { get; set; } = new List<City>();

    private City? _selectedCity;

    public City? SelectedCity
    {
        get => _selectedCity;
        set => SetProperty(ref _selectedCity, value);
    }

    public RelayCommand FilterCommand { get; set; }
    public RelayCommand DetailCommand { get; set; }

    #endregion

    #region Operations

    private async Task FilterAsync()
    {
        await Load(_uow);
    }

    private async Task DetailAsync()
    {
        if (Controller != null && SelectedStation != null)
        {
            await Controller.ShowStationWindowAsync(SelectedStation);
        }

        await Load(_uow);
    }

    public override async Task InitializeDataAsync()
    {
        Cities = await _uow.CityRepository.GetNoTrackingAsync(orderBy: x => x.OrderBy(c => c.Name));
        OnPropertyChanged(nameof(Cities));
        await Task.Delay(1);
        SelectedCity = Cities.FirstOrDefault(c => c.Name.Contains("Linz"));
        await Load(_uow);
    }

    public async Task Load(IUnitOfWork uow)
    {
        Expression<Func<Station, bool>>? filter = null;

        if (SelectedCity is not null)
        {
            filter = x => x.CityId == SelectedCity.Id;
        }

        var filtered = await uow.StationRepository.GetNoTrackingAsync(
            filter: filter,
            stations => stations.OrderBy(x => x.Name),
            includeProperties: nameof(Station.City)
        );

        FilteredStations.Clear();
        foreach (var filteredStation in filtered)
        {
            FilteredStations.Add(filteredStation);
        }
    }

    #endregion
}