namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

public class HikeHighlightsViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public HikeHighlightsViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public int? HikeId { get; set; }

    private Hike? _hike;

    public Hike? Hike
    {
        get => _hike;
        set => SetProperty(ref _hike, value);
    }

    public ObservableCollection<Models.Highlight> Highlights { get; } = new();

    public RelayCommand CloseCommand => new RelayCommand(() => Controller?.CloseWindow(), () => true);

    #endregion

    #region Operations

    public override async Task InitializeDataAsync()
    {
        await Load(_uow);
    }


    public async Task Load(IUnitOfWork uow)
    {
        var hike = await uow.HikeRepository.GetByIdAsync(HikeId!.Value, nameof(Hike.Highlights));

        Highlights.Clear();

        if (hike is not null)
        {
            Hike = hike;
            var no = 1;
            foreach (var highlight in hike.Highlights!)
            {
                Highlights.Add(new Models.Highlight()
                {
                    Id = highlight.Id,
                    No = no++,
                    Description = highlight.Description,
                    Comment = highlight.Comment
                });
            }
        }
    }

    #endregion
}