using Core.Contracts;
using Core.Entities.Visitors;
using Microsoft.Win32;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMvvmBase;

namespace WpfTadeotAdmin.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private IUnitOfWork _uow;

        public MainViewModel() : this(null)
        {

        }

        public MainViewModel(IWindowController? controller) : base(controller)
        {
            _uow = new UnitOfWork();
            // TODO: Create RelayCommands
        }

        public async Task LoadDataAsync()
        {
            // TODO: Load data from uow for data binding         
        }
    }
}
