namespace Wpf.ViewModels;

using Base.WpfMvvm;

public interface IWindowNavigator : IBaseWindowNavigator
{
    public Task ShowBacklogCommentWindowAsync(int backlogItemId);

    public Task ShowImportTurtleWindowAsync();
}