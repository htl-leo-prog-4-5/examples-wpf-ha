using Base.WpfMvvm;

namespace Wpf;

public interface IWindowNavigator : IBaseWindowNavigator
{
    string? ShowFileOpenDialog();
}