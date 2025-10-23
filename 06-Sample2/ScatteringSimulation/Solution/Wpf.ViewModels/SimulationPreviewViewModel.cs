namespace Wpf.ViewModels;

using System;
using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class SimulationPreviewViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public SimulationPreviewViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public int? SimulationId { get; set; }

    private Simulation? _simulation;

    public Simulation? Simulation
    {
        get => _simulation;
        set => SetProperty(ref _simulation, value);
    }

    private IList<(double X, double Y, double Value)> _samples = [];

    public IList<(double X, double Y, double Value)> Samples
    {
        get => _samples;
        set => SetProperty(ref _samples, value);
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
        var simulation = await uow.SimulationRepository.GetByIdAsync(SimulationId!.Value, nameof(Simulation.Samples));

        if (simulation is not null)
        {
            Simulation = simulation;
            Samples    = simulation.Samples!.Select(s => (s.X, s.Y, s.Value)).ToList();
        }
    }

    #endregion
}