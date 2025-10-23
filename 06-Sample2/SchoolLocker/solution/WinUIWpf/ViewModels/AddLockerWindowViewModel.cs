using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using WpfMvvmBase;

namespace WinUIWpf.ViewModels;

public class AddLockerWindowViewModel : BaseViewModel
{
    #region crt

    public AddLockerWindowViewModel(IUnitOfWork uow)
    {
        _uow = uow;
        SaveLockerCommand = new RelayCommand(
            async _ => await SaveLockerAsync(),
            _ => string.IsNullOrEmpty(ValidationError));
    }

    private IUnitOfWork _uow;

    #endregion

    #region Properties

    private string? _validationError;

    public string? ValidationError
    {
        get => _validationError;
        set => SetProperty(ref _validationError, value);
    }

    private string _numberText = "";

    public string NumberText
    {
        get => _numberText;
        set
        {
            SetProperty(ref _numberText!, value);
            ValidationError = Validate();
        }
    }

    private string? Validate()
    {
        if (_numberText.Length == 3 && _numberText.All(c => char.IsDigit(c)))
        {
            return null;
        }

        return "Spindnummer muss exakt 3 Ziffern lang sein";
    }

    #endregion

    #region Commands

    public ICommand SaveLockerCommand { get; set; }

    #endregion

    #region Operations

    public async Task SaveLockerAsync()
    {
        try
        {
            var locker = new Locker
            {
                Number = int.Parse(NumberText)
            };
            await _uow.Lockers.AddAsync(locker);
            await _uow.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ValidationError = $"Ein Spind mit dieser Nummer existiert bereits!";
            return;
        }

        Controller!.CloseWindow();
    }

    #endregion
}