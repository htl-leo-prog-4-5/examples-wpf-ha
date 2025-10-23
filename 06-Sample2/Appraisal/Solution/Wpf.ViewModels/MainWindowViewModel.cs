namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public           IWindowNavigator? Controller { get; set; }
    private readonly IUnitOfWork       _uow;
    private readonly IImportService    _importService;

    public MainWindowViewModel(IUnitOfWork uow, IImportService importService)
    {
        _uow           = uow;
        _importService = importService;
    }

    #endregion

    #region Properties/Commands

    private bool _isMissingNames = true;

    public bool IsMissingNames
    {
        get => _isMissingNames;
        set => SetProperty(ref _isMissingNames, value);
    }

    public ObservableCollection<PatientOverview> FilteredPatients { get; } = new();

    private PatientOverview? _selectedPatient;

    public PatientOverview? SelectedPatient
    {
        get => _selectedPatient;
        set
        {
            if (SetProperty(ref _selectedPatient, value))
            {
                FirstName  = _selectedPatient?.FirstName;
                LastName   = _selectedPatient?.LastName;
                IsEditMode = false;
            }
        }
    }

    private string? _firstName;

    public string? FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    private string? _lastName;

    public string? LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
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


    public RelayCommand FilterCommand       => new RelayCommand(async () => await FilterAsync(),       () => true);
    public RelayCommand ExaminationsCommand => new RelayCommand(async () => await ExaminationsAsync(), () => SelectedPatient != null);
    public RelayCommand EditCommand         => new RelayCommand(Edit,                                  () => SelectedPatient != null && IsNotEditMode);
    public RelayCommand UpdateCommand       => new RelayCommand(async () => await UpdateAsync(),       () => IsEditMode);
    public RelayCommand ImportCommand       => new RelayCommand(async () => await ImportAsync());

    #endregion

    #region Operations

    private void Edit()
    {
        IsEditMode = true;
    }

    private async Task UpdateAsync()
    {
        var inDb = (await _uow.PatientRepository.GetByIdAsync(SelectedPatient!.Id)) ?? throw new ArgumentNullException();
        inDb.FirstName = FirstName;
        inDb.LastName  = LastName;
        await _uow.SaveChangesAsync();
        await InitializeDataAsync();

        IsEditMode = false;
    }

    private async Task FilterAsync()
    {
        await Load(_uow);
    }

    private async Task ExaminationsAsync()
    {
        if (Controller != null && SelectedPatient != null)
        {
            await Controller.ShowExaminationsWindowAsync(SelectedPatient.Id);
        }

        await Load(_uow);
    }

    private async Task ImportAsync()
    {
        int newCount = await _importService.ImportAsync("ImportData/Wpf");
        if (newCount > 0)
        {
            Controller?.ShowMessageBox($"{newCount} new examinations imported!");
            await Load(_uow);
        }
        else
        {
            Controller?.ShowMessageBox($"no new new examinations found!");
        }
    }

    public override async Task InitializeDataAsync()
    {
        await Load(_uow);
    }

    public async Task Load(IUnitOfWork uow)
    {
        var filtered = await uow.PatientRepository
            .GetPatientOverviewAsync(IsMissingNames);

        FilteredPatients.Clear();
        foreach (var filteredStation in filtered)
        {
            FilteredPatients.Add(filteredStation);
        }
    }

    #endregion
}