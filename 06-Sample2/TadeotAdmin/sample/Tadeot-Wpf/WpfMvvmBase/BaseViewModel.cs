namespace WpfMvvmBase
{
    public  class BaseViewModel : NotifyPropertyChanged
    {
        public IWindowController? Controller { get; }

        public BaseViewModel(IWindowController? controller)
        {
            Controller = controller;
        }
    }
}
