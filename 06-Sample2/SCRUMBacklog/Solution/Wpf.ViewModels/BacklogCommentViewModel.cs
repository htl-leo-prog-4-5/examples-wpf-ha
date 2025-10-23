namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

public class BacklogCommentViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public BacklogCommentViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public int? BacklogItemId { get; set; }

    private BacklogItem? _backlogItem;

    public BacklogItem? BacklogItem
    {
        get => _backlogItem;
        set => SetProperty(ref _backlogItem, value);
    }

    public ObservableCollection<Models.Comment> Comments { get; } = new();

    public RelayCommand CloseCommand => new RelayCommand(() => Controller?.CloseWindow(), () => true);

    #endregion

    #region Operations

    public override async Task InitializeDataAsync()
    {
        await Load(_uow);
    }


    public async Task Load(IUnitOfWork uow)
    {
        var backlogItem = await uow.BacklogItemRepository.GetByIdAsync(BacklogItemId!.Value, nameof(BacklogItem.Comments));

        Comments.Clear();

        if (backlogItem is not null)
        {
            BacklogItem = backlogItem;
            var no = 1;
            foreach (var comment in backlogItem.Comments!.OrderByDescending(h => h.SeqNo))
            {
                Comments.Add(new Models.Comment()
                {
                    Id          = comment.Id,
                    No          = no++,
                    SeqNo       = comment.SeqNo,
                    Description = comment.Description,
                });
            }
        }
    }

    #endregion
}