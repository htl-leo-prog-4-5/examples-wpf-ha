using Core.Contracts;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmBase;

namespace WpfTadeotAdmin.ViewModels
{
    public class RegistrationConfigViewModel : BaseViewModel
    {
        private IUnitOfWork _uow;

        public RegistrationConfigViewModel() : this(null, new UnitOfWork())
        {

        }
        public RegistrationConfigViewModel(IWindowController? controller, IUnitOfWork uow) : base(controller)
        {
            _uow = uow;
        }

        public Task LoadDataAsync()
        {
            throw new NotImplementedException();
        }

    }
}
