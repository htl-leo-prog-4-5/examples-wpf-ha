namespace WinUIWpf.ViewModels;

using Base.WpfMvvm;

using System.Threading.Tasks;

using Core.DataTransferObjects;

public interface IWindowNavigator : IBaseWindowNavigator
{
    Task ShowCheckinRoomAsync(RoomDto room);

    Task ShowCheckoutRoomAsync(RoomDto room);
}