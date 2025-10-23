using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Logic.Contracts;

namespace Enterprise.WPF.ViewModels
{
    public class MainWindowsViewModel
    {
        public MainWindowsViewModel(IMyService service)
        {
            _service = service;
        }

        public int GetZero()
        {
            return _service.GetZero();
        }

        private IMyService _service;
    }
}
