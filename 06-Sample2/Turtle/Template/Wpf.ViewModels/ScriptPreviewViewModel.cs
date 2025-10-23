namespace Wpf.ViewModels;

using Base.WpfMvvm;

using Core.Contracts;

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

    private IList<Move> _moves = [];

    public IList<Move> Moves
    {
        get => _moves;
        set => SetProperty(ref _moves, value);
    }

    #endregion

    #region Operations

    public override async Task InitializeDataAsync()
    {
        await Task.CompletedTask; // avoid cs1998
    }

    #endregion
}