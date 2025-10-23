namespace Wpf.ViewModels;

using System.Collections.ObjectModel;
using System.Linq.Expressions;

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
    }

    #endregion

    #region Properties/Commands

    public ObservableCollection<BacklogItemOverview> FilteredBacklogItems { get; } = new();

    private BacklogItemOverview? _selectedBacklogItem;

    public BacklogItemOverview? SelectedBacklogItem
    {
        get => _selectedBacklogItem;
        set
        {
            if (SetProperty(ref _selectedBacklogItem, value))
            {
                Priority = _selectedBacklogItem?.Priority;

                IsEditMode = false;
            }
        }
    }

    public IList<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    private TeamMember? _selectedTeamMember;

    public TeamMember? SelectedTeamMember
    {
        get => _selectedTeamMember;
        set => SetProperty(ref _selectedTeamMember, value);
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

    private int? _priority;

    public int? Priority
    {
        get => _priority;
        set => SetProperty(ref _priority, value);
    }

    public RelayCommand FilterCommand => new RelayCommand(async () => await FilterAsync(), () => true);
    public RelayCommand DetailCommand => new RelayCommand(async () => await DetailAsync(), () => SelectedBacklogItem != null);
    public RelayCommand EditCommand   => new RelayCommand(Edit,                            () => SelectedBacklogItem != null && IsNotEditMode);
    public RelayCommand UpdateCommand => new RelayCommand(async () => await Update(),      () => IsEditMode);

    #endregion

    #region Operations

    private void Edit()
    {
        IsEditMode = true;
    }

    private async Task Update()
    {
        var inDb = (await _uow.BacklogItemRepository.GetByIdAsync(SelectedBacklogItem!.Id)) ?? throw new ArgumentNullException();
        inDb.Priority = Priority;
        await _uow.SaveChangesAsync();
        await InitializeDataAsync();

        IsEditMode = false;
    }

    private async Task FilterAsync()
    {
        await Load(_uow);
    }

    private async Task DetailAsync()
    {
        if (Controller != null && SelectedBacklogItem != null)
        {
            await Controller.ShowBacklogCommentWindowAsync(SelectedBacklogItem.Id);
        }

        await Load(_uow);
    }

    public override async Task InitializeDataAsync()
    {
        TeamMembers = await _uow.TeamMemberRepository.GetNoTrackingAsync(orderBy: x => x.OrderBy(c => c.Name));

        var all = new TeamMember()
        {
            Name = "<All>"
        };

        TeamMembers.Add(all);
        OnPropertyChanged(nameof(TeamMembers));
        await Task.Delay(1);
        SelectedTeamMember = all;
        await Load(_uow);
    }

    public async Task Load(IUnitOfWork uow)
    {
        var filtered = await uow.BacklogItemRepository
            .GetBacklogAsync(
                SelectedTeamMember != null && SelectedTeamMember.Id > 0 ? SelectedTeamMember.Name : null);

        FilteredBacklogItems.Clear();
        foreach (var filteredStation in filtered)
        {
            FilteredBacklogItems.Add(filteredStation);
        }
    }

    #endregion
}