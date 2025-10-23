using System.Windows;

namespace WpfMvvmBase 
{
    public class WindowController : IWindowController
    {
        private readonly Window _window;

        public WindowController(Window window)
        {
            _window = window;
        }
        public void ShowWindow(BaseViewModel viewModel)
        {
            _window.DataContext = viewModel;
            _window.Show();
        }

        public void ShowDialog(BaseViewModel viewModel)
        {
            _window.DataContext = viewModel;
            _window.ShowDialog();
        }

        public void CloseWindow()
        {
            _window.Close();
        }
    }
}
