namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }

    public MainWindowViewModel(IUnitOfWork uow)
    {
    }

    #endregion

    #region Properties/Commands


    #endregion

    #region Operations

    public override async Task InitializeDataAsync()
    {
    }

    #endregion
}