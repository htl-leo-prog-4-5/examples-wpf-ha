namespace WinUIWpf.ViewModels;

using Core.Contracts;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Entities;

public class MainViewModel : BaseViewModel
{
    #region ctr

    public MainViewModel()
    {
    }


    #endregion

    #region Properties / Commands

    public IWindowNavigator? Controller { get; set; }

    #endregion

    #region Operations

    public async Task LoadDataAsync()
    {
    }

    #endregion
}