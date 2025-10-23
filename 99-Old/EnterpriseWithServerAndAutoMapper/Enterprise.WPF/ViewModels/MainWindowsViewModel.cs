using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Enterprise.Service.Contracts;
using Enterprise.WPF.Helpers;
using Enterprise.WPF.Models;

namespace Enterprise.WPF.ViewModels
{
    public class MainWindowsViewModel : INotifyPropertyChanged
    {
        public MainWindowsViewModel(IMyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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

        private MyInfo _info = new MyInfo();

        public MyInfo MyInfo
        {
            get => _info;
            set
            {
                _info = value;
                RaisePropertyChanged();
            }
        }

        public async Task<MyInfo> GetInfo()
        {
            var infoDTO = await _service.GetMyInfo();
            return _mapper.Map<MyInfo>(infoDTO);
        }

        private IMyService _service;
        private IMapper _mapper;

        public ICommand CallMyService => new DelegateCommand(async () => ServiceValue = await GetZero());
        public ICommand CallMyInfoService => new DelegateCommand(async () => MyInfo = await GetInfo());
    }
}
