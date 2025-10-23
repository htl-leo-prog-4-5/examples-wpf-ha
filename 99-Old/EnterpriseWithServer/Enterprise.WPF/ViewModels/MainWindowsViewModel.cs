using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Enterprise.Service.Contracts;
using Enterprise.WPF.Helpers;

namespace Enterprise.WPF.ViewModels
{
    public class MainWindowsViewModel : INotifyPropertyChanged
    {
        public MainWindowsViewModel(IMyService service)
        {
            _service = service;
        }
        #region INPC 

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

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

        public async Task<int> GetZero()
        {
            return await _service.GetZero();
        }

        private IMyService _service;

        public ICommand CallMyService => new DelegateCommand(async () => ServiceValue = await GetZero());
    }
}
