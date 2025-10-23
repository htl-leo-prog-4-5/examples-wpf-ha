namespace WpfMvvmBase;

public interface IWindowNavigator
{
    void OpenRegistrationConfigDialog();

    bool AskDeleteVisitors();

    string? AskSaveToCsvFile();

    void CloseWindow();
}