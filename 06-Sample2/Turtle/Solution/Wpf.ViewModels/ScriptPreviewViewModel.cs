namespace Wpf.ViewModels;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;
using Core.Turtle;

using Move = Core.Turtle.Move;

public class ScriptPreviewViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public ScriptPreviewViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public int? ScriptId { get; set; }

    private Script? _script;

    public Script? Script
    {
        get => _script;
        set => SetProperty(ref _script, value);
    }

    private IList<Move> _moves = [];

    public IList<Move> Moves
    {
        get => _moves;
        set => SetProperty(ref _moves, value);
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
        var script = await uow.ScriptRepository.GetNoTrackingByIdAsync(ScriptId!.Value, nameof(Script.Moves));

        if (script is not null)
        {
            Script = script;
            Moves  = script.Moves!.Select(s => new Move((Direction)s.Direction, s.Repeat, s.Color)).ToList();
        }
    }

    #endregion
}