namespace WpfMvvmBase
{
    public interface IWindowNavigator
    {
        void ShowAddLockerWindow();
        void ShowBookingWindow(int lockerNo);
        void CloseWindow();
    }
}