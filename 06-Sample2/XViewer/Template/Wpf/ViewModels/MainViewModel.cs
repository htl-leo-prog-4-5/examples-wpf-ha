using System.Collections.ObjectModel;
using Base.WpfMvvm;
using Core.Draw;
using Core.Entities;

namespace Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region crt

        public MainViewModel()
        {
        }

        #region Properties/Commands

        public IWindowNavigator? Controller { get; set; }


        #endregion


        #endregion

        #region Operations

        public async override Task InitializeDataAsync()
        {
            await Task.CompletedTask;
        }


        #endregion
    }
}