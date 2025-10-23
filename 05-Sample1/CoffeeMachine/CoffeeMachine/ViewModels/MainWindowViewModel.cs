using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using CoffeeMachine.Helpers;
using CoffeeMachine.Models;

namespace CoffeeMachine.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region GUI Forward

        public Action ShowSetupDlg { get; set; }
        public Action SellSetupDlg { get; set; }
        public Action CloseAction { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Operations

        #endregion

        public ICommand SetupCommand => new DelegateCommand(() => ShowSetupDlg?.Invoke(),() => ShowSetupDlg != null);
        public ICommand SellCommand => new DelegateCommand(() => SellSetupDlg?.Invoke(), () => SellSetupDlg != null);
    }
}
