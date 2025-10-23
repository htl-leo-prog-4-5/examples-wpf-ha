namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class StationViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public StationViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public int? StationId { get; set; }

    private Station? _station;

    public Station? RailwayStation
    {
        get => _station;
        set => SetProperty(ref _station, value);
    }

    public ObservableCollection<RailwayCompany> RailwayCompanies { get; } = new();

    private RailwayCompany? _selectedCompany;

    public RailwayCompany? SelectedCompany
    {
        get => _selectedCompany;
        set
        {
            if (SetProperty(ref _selectedCompany, value))
            {
                IsEditMode = false;

                City     = _selectedCompany?.City;
                PLZ      = _selectedCompany?.PLZ;
                Street   = _selectedCompany?.Street;
                StreetNo = _selectedCompany?.StreetNo;
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

    private string? _city;

    public string? City
    {
        get => _city;
        set => SetProperty(ref _city, value);
    }

    private string? _plz;

    public string? PLZ
    {
        get => _plz;
        set => SetProperty(ref _plz, value);
    }

    private string? _street;

    public string? Street
    {
        get => _street;
        set => SetProperty(ref _street, value);
    }

    private string? _streetNo;

    public string? StreetNo
    {
        get => _streetNo;
        set => SetProperty(ref _streetNo, value);
    }


    public RelayCommand CloseCommand  => new RelayCommand(() => Controller?.CloseWindow(), () => true);
    public RelayCommand EditCommand   => new RelayCommand(Edit,                            () => SelectedCompany != null && IsNotEditMode);
    public RelayCommand UpdateCommand => new RelayCommand(async () => await Update(),      () => IsEditMode);

    #endregion

    #region Operations

    private void Edit()
    {
        IsEditMode = true;
    }

    private async Task Update()
    {
        var inDb = (await _uow.RailwayCompanyRepository.GetByIdAsync(SelectedCompany!.Id)) ?? throw new ArgumentNullException();
        inDb.City     = City;
        inDb.PLZ      = PLZ;
        inDb.Street   = Street;
        inDb.StreetNo = StreetNo;
        await _uow.SaveChangesAsync();
        await InitializeDataAsync();

        IsEditMode = false;
    }

    public override async Task InitializeDataAsync()
    {
        await Load(_uow);
    }

    public async Task Load(IUnitOfWork uow)
    {
        var station = await uow.StationRepository.GetByIdAsync(StationId!.Value, nameof(Station.RailwayCompanies), nameof(Station.Infrastructures), nameof(Station.Lines), nameof(Core.Entities.City));

        RailwayCompanies.Clear();

        if (station is not null)
        {
            RailwayStation = station;
            foreach (var company in station.RailwayCompanies!)
            {
                RailwayCompanies.Add(company);
            }
        }
    }

    #endregion
}