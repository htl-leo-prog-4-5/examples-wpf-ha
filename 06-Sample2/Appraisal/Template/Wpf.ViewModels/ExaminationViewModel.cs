namespace Wpf.ViewModels;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class ExaminationViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public ExaminationViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public int? PatientId { get; set; }

    private Patient? _patient;

    public Patient? Patient
    {
        get => _patient;
        set => SetProperty(ref _patient, value);
    }

    #endregion

    #region Operations

    public override async Task InitializeDataAsync()
    {
    }

    #endregion
}