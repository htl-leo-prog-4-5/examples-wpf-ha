namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public MainWindowViewModel(IUnitOfWork uow)
    {
        _uow = uow;

        ShowDetailCommand = new RelayCommand(async () => await ShowDetail(), () => SelectedCompany != null);
    }

    #endregion

    #region Properties/Commands

    public ObservableCollection<CompanyOverview> Companies { get; } = new();

    private CompanyOverview? _selectedCompany;

    public CompanyOverview? SelectedCompany
    {
        get => _selectedCompany;
        set
        {
            if (SetProperty(ref _selectedCompany, value))
            {
                // OnPropertyChanged(nameof(OtherProp));
            }
        }
    }

    public RelayCommand ShowDetailCommand { get; set; }

    #endregion

    #region Operations

    public async Task ShowDetail()
    {
        if (SelectedCompany == null)
        {
            return;
        }

        await Controller!.ShowDetailAsync(SelectedCompany.Id);
    }

    public override async Task InitializeDataAsync()
    {
        await LoadCompanies(_uow);
    }

    public async Task LoadCompanies(IUnitOfWork uow)
    {
        var filtered = await uow.ShippingCompanyRepository.GetOverviewAsync();

        Companies.Clear();
        foreach (var company in filtered)
        {
            Companies.Add(company);
        }
    }

    #endregion
}