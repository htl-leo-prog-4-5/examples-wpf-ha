namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

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

    private ObservableCollection<Examination> _examinations = [];

    public ObservableCollection<Examination> Examinations
    {
        get => _examinations;
        set => SetProperty(ref _examinations, value);
    }


    private Examination? _selectedExamination;

    public Examination? SelectedExamination
    {
        get => _selectedExamination;
        set => SetProperty(ref _selectedExamination, value);
    }

    public RelayCommand CloseCommand => new RelayCommand(() => Controller?.CloseWindow(), () => true);

    #endregion

    #region Operations

    public override async Task InitializeDataAsync()
    {
        await Load(_uow);
    }

    public async Task Load(IUnitOfWork uow)
    {
        var patient = await uow.PatientRepository.GetNoTrackingByIdAsync(PatientId!.Value, nameof(Patient.Examinations), $"{nameof(Patient.Examinations)}.{nameof(Examination.DataStreams)}");

        if (patient is not null)
        {
            Patient = patient;
            Examinations.Clear();
            foreach (var examination in patient.Examinations!)
            {
                Examinations.Add(examination);
            }

            SelectedExamination = Examinations.FirstOrDefault();
        }
    }

    #endregion
}