using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeMachine.Helpers;
using CoffeeMachine.Models;

namespace CoffeeMachine.ViewModels
{
    class SetupViewModel : ViewModelBase
    {
        #region GUI Forward

        public Func<string, bool, string> BrowseFileNameFunc { get; set; }
        public Action CloseAction { get; set; }

        #endregion

        #region Properties

        private string _filename = new CoffeeMachineManager().CurrentFileName;
        public string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                RaisePropertyChanged(); 
            }
        }

        private CoinModel _model = new CoinModel();
        public CoinModel Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Operations


        void ReadFromFile()
        {
            var mgr = new CoffeeMachineManager();
            var coins = mgr.ReadFromFile(Filename);

            Model.SetCoinValue(coins);
        }
        void WriteToFile()
        {
            var mgr = new CoffeeMachineManager();
            mgr.WriteToFile(Filename,Model.GetCoins());
       }

        void BrowserFile()
        {
            var filename = BrowseFileNameFunc?.Invoke(Filename, true);
            if (filename != null)
                Filename = filename;
        }

        #endregion

        public ICommand ReadFromFileCommand => new DelegateCommand(ReadFromFile, () => System.IO.File.Exists(Environment.ExpandEnvironmentVariables(Filename)));
        public ICommand WriteToFileCommand => new DelegateCommand(WriteToFile, () => Model.Coin1Value != 0);
        public ICommand GetCurrentCoinsCommand => new DelegateCommand(() => Model.SetCoinValue(new CoffeeMachineManager().CurrentCoins));
        public ICommand CloseCommand => new DelegateCommand(() => CloseAction?.Invoke());
        public ICommand BrowseFileCommand => new DelegateCommand(BrowserFile);

    }
}
