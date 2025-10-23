using System.Collections.ObjectModel;
using Style4.Models;
using Style4.Tools;

namespace Style4.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    #region Properties

    public ObservableCollection<MyData> SomeData { get; } = new()
    {
        new MyData() {MyName = "Hallo", MyInt = 4711},
        new MyData() {MyName = "Welt", MyInt = 2022},
        new MyData() {MyName = "Hello", MyInt = 2023},
        new MyData() {MyName = "World", MyInt = 1900},
    };

    #endregion

    #region Operations

    #endregion

    #region Commands

    #endregion
}