namespace WpfMvvmBase
{
    public interface IWindowController
    {
        void ShowWindow(BaseViewModel viewModel);
        void ShowDialog(BaseViewModel viewModel);
        void CloseWindow();
    }
}
