namespace WinUIWpf.ViewModels;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

using Persistence;

public class RoomTypeDescription
{
    public RoomType? RoomType    { get; set; }
    public string    Description { get; set; } = string.Empty;
}

public class MainWindowViewModel : BaseViewModel
{
    public IWindowNavigator Controller { get; set; }

    public List<RoomTypeDescription> RoomTypes { get; } = new()
    {
        new() { RoomType = null, Description              = "Alle" },
        new() { RoomType = RoomType.Standard, Description = RoomType.Standard.ToString() },
        new() { RoomType = RoomType.Premium, Description  = RoomType.Premium.ToString() },
        new() { RoomType = RoomType.Deluxe, Description   = RoomType.Deluxe.ToString() },
        new() { RoomType = RoomType.Suite, Description    = RoomType.Suite.ToString() },
    };


    public MainWindowViewModel(IWindowNavigator windowController)
    {
        Controller        = windowController;
    }


    public async Task LoadDataAsync()
    {
    }
}