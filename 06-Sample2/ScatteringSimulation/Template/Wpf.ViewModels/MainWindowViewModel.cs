namespace Wpf.ViewModels;

using System.Collections.ObjectModel;
using System.IO;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public           IWindowNavigator? Controller { get; set; }
    private readonly IUnitOfWork       _uow;

    public MainWindowViewModel(IUnitOfWork uow)
    {
        _uow    = uow;
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