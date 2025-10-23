using Core.Contracts;

using System;
using System.Linq;
using System.Threading.Tasks;

using WpfMvvmBase;

namespace WpfTadeotAdmin.ViewModels;

public class RegistrationConfigViewModel : BaseViewModel
{
    private readonly IUnitOfWork _uow;

    private string? _dbReasons;
    private string? _reasonsText;

    public string? ReasonsText
    {
        get => _reasonsText;
        set
        {
            _reasonsText = value;
            OnPropertyChanged();
        }
    }

    private string? _dbTypes;
    private string? _typesText;

    public string? TypesText
    {
        get => _typesText;
        set
        {
            _typesText = value;
            OnPropertyChanged();
        }
    }

    public RegistrationConfigViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task LoadDataAsync()
    {
        await LoadReasonsAsync();

        await LoadSchoolTypesAsync();
    }

    private async Task LoadSchoolTypesAsync()
    {
        var types = await _uow.SchoolTypes.GetAsync(null,
            r => r.OrderBy(type => type.Rank));
        var texts = types.Select(t => t.Type).ToArray();
        _dbTypes  = String.Join('\n', texts);
        TypesText = _dbTypes;
    }

    private async Task LoadReasonsAsync()
    {
        var reasons = await _uow.ReasonsForVisit
            .GetAsync(null,
                r => r.OrderBy(reason => reason.Rank));
        var texts = reasons.Select(r => r.Reason).ToArray();

        _dbReasons  = string.Join('\n', texts);
        ReasonsText = _dbReasons;
    }
}