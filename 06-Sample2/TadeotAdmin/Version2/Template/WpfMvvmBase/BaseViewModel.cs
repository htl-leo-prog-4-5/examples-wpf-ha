namespace WpfMvvmBase;

public class BaseViewModel : NotifyPropertyChanged
{
    public IWindowNavigator? Controller { get; set; }
}