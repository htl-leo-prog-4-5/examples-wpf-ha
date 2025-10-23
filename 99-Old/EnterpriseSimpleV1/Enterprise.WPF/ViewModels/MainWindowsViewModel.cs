using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Enterprise.Logic.Contracts;
using Enterprise.Shared;
using Enterprise.WPF.Helpers;

namespace Enterprise.WPF.ViewModels
{
    public class MainWindowsViewModel : INotifyPropertyChanged
    {
        private IFactory<IMyService> _serviceFactory;

        public MainWindowsViewModel(IFactory<IMyService> serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        }

        #region INPC 

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        private int _serviceValue = 4711;

        public int ServiceValue
        {
            get => _serviceValue;
            set
            {
                _serviceValue = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Operations

        public async Task<int> GetZero()
        {
            using (var scope = _serviceFactory.Create())
            {
                return await scope.Instance.GetZero();
            }
        }

        #endregion

        #region Commands

        public ICommand CallMyService => new DelegateCommand(async () => ServiceValue = await GetZero());

        #endregion
    }
}