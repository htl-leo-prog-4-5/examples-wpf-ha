namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class NewTripViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public NewTripViewModel(IUnitOfWork uow)
    {
        _uow = uow;

        NewCommand   = new RelayCommand(async () => await NewAsync(),    () => SelectedRoute != null);
        CloseCommand = new RelayCommand(() => Controller?.CloseWindow(), () => true);
    }

    #endregion

    #region Properties/Commands

    private DateTime _dateFrom = DateTime.Today;

    public DateTime DateFrom
    {
        get => _dateFrom;
        set => SetProperty(ref _dateFrom, value);
    }

    private DateTime _dateTo = DateTime.Today + TimeSpan.FromDays(7);

    public DateTime DateTo
    {
        get => _dateTo;
        set => SetProperty(ref _dateTo, value);
    }

    private DateTime _drawDate = DateTime.Today + TimeSpan.FromDays(8);

    public DateTime DrawDate
    {
        get => _drawDate;
        set => SetProperty(ref _drawDate, value);
    }

    private Route? _selectedRoute;

    public Route? SelectedRoute
    {
        get => _selectedRoute;
        set => SetProperty(ref _selectedRoute, value);
    }

    private IList<Route> _routes = new List<Route>();

    public IList<Route> Routes
    {
        get => _routes;
        set => SetProperty(ref _routes, value);
    }

    public RelayCommand NewCommand   { get; set; }
    public RelayCommand CloseCommand { get; set; }

    #endregion

    #region Operations

    private async Task NewAsync()
    {
        if (DateTo <= DateFrom)
        {
            Controller!.ShowMessageBox($"Error: Date-From(${DateFrom.ToShortDateString()}) is after Date-To({DateTo.ToShortDateString()})");
        }
        else if (DateTo < DateTime.Today)
        {
            Controller!.ShowMessageBox($"Error: Date-From({DateFrom.ToShortDateString()}) must be in the future ({DateTime.Today.ToShortDateString()})");
        }
        else
        {
            var newGame = new Trip()
            {
                DepartureDateTime = DateFrom,
                ArrivalDateTime   = DateTo,
                Route             = SelectedRoute,
            };

            await _uow.TripRepository.AddAsync(newGame);
            await _uow.SaveChangesAsync();
            Controller!.CloseWindow();
        }
    }

    public override async Task InitializeDataAsync()
    {
        Routes = await _uow.RouteRepository.GetAsync();
    }

    #endregion
}