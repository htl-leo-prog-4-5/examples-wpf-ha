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
    public class SellViewModel : ViewModelBase
    {
        #region GUI Forward

        public Func<string, bool> MsgBox { get; set; }
        public Action CloseAction { get; set; }

        #endregion

        #region Properties

        private decimal _sellprice = 0.7m;
        public decimal SellPrice
        {
            get => _sellprice;
            set
            {
                _sellprice = value;
                RaisePropertyChanged(); 
            }
        }

        private CoinModel _modelIn = new CoinModel()
        {
            Coin1Value = 0.1m,
            Coin2Value = 0.2m,
            Coin3Value = 0.5m,
            Coin4Value = 1m,
            Coin5Value = 2m
        };
        public CoinModel ModelIn
        {
            get => _modelIn;
            set
            {
                _modelIn = value;
                RaisePropertyChanged();
            }
        }
        private CoinModel _modelOut = new CoinModel();
        public CoinModel ModelOut
        {
            get => _modelOut;
            set
            {
                _modelOut = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Operations

        IEnumerable<CoffeeMachineManager.Coins> GetCoins()
        {
            return new []
            {
                new CoffeeMachineManager.Coins {Value = ModelIn.Coin1Value, Amount = ModelIn.Coin1Amount},
                new CoffeeMachineManager.Coins {Value = ModelIn.Coin2Value, Amount = ModelIn.Coin2Amount},
                new CoffeeMachineManager.Coins {Value = ModelIn.Coin3Value, Amount = ModelIn.Coin3Amount},
                new CoffeeMachineManager.Coins {Value = ModelIn.Coin4Value, Amount = ModelIn.Coin4Amount},
                new CoffeeMachineManager.Coins {Value = ModelIn.Coin5Value, Amount = ModelIn.Coin5Amount},
            };
        }

        void Sell()
        {
            var mgr = new CoffeeMachineManager();

            var givenCoins = ModelIn.GetCoins();

            try
            {
                var returns = mgr.Sell(SellPrice, givenCoins);
                ModelOut.SetCoinValue(returns);
            }
            catch (Exception e)
            {
                MsgBox?.Invoke(e.Message);
            }
        }

        #endregion

        public ICommand SellCommand => new DelegateCommand(Sell);
        public ICommand CloseCommand => new DelegateCommand(() => CloseAction?.Invoke());

    }
}
