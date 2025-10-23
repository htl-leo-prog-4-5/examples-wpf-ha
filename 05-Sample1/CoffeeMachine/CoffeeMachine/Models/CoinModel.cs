using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Helpers;

namespace CoffeeMachine.Models
{
    public class CoinModel : BindableBase
    {
        protected void RaisePropertyChangedForAll([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChanged(propertyName);
            RaisePropertyChanged(nameof(CoinTotalAmount));

        }

        private decimal _coin1Value;
        public decimal Coin1Value
        {
            get => _coin1Value;
            set
            {
                _coin1Value = value;
                RaisePropertyChangedForAll();
            }
        }
        private uint _coin1Amount;
        public uint Coin1Amount
        {
            get => _coin1Amount;
            set
            {
                _coin1Amount = value;
                RaisePropertyChangedForAll();
            }
        }

        private decimal _coin2Value;
        public decimal Coin2Value
        {
            get => _coin2Value;
            set
            {
                _coin2Value = value;
                RaisePropertyChangedForAll();
            }
        }
        private uint _coin2Amount;
        public uint Coin2Amount
        {
            get => _coin2Amount;
            set
            {
                _coin2Amount = value;
                RaisePropertyChangedForAll();
            }
        }

        private decimal _coin3Value;
        public decimal Coin3Value
        {
            get => _coin3Value;
            set
            {
                _coin3Value = value;
                RaisePropertyChangedForAll();
            }
        }
        private uint _coin3Amount;
        public uint Coin3Amount
        {
            get => _coin3Amount;
            set
            {
                _coin3Amount = value;
                RaisePropertyChangedForAll();
            }
        }

        private decimal _coin4Value;
        public decimal Coin4Value
        {
            get => _coin4Value;
            set
            {
                _coin4Value = value;
                RaisePropertyChangedForAll();
            }
        }
        private uint _coin4Amount;
        public uint Coin4Amount
        {
            get => _coin4Amount;
            set
            {
                _coin4Amount = value;
                RaisePropertyChangedForAll();
            }
        }

        private decimal _coin5Value;
        public decimal Coin5Value
        {
            get => _coin5Value;
            set
            {
                _coin5Value = value;
                RaisePropertyChangedForAll();
            }
        }
        private uint _coin5Amount;
        public uint Coin5Amount
        {
            get => _coin5Amount;
            set
            {
                _coin5Amount = value;
                RaisePropertyChangedForAll();
            }
        }

        public decimal CoinTotalAmount =>
            Coin1Value * Coin1Amount +
            Coin2Value * Coin2Amount +
            Coin3Value * Coin3Amount +
            Coin4Value * Coin4Amount +
            Coin5Value * Coin5Amount;


        public void SetCoinValue(IEnumerable<CoffeeMachineManager.Coins> coins)
        {
            var coinsArray = coins.ToArray();
            var length = coinsArray != null ? coinsArray.Length : 0;
            if (length > 0)
            {
                Coin1Value = coinsArray[0].Value;
                Coin1Amount = coinsArray[0].Amount;
            }
            else
            {
                Coin1Value = 0;
                Coin1Amount = 0;
            }
            if (length > 1)
            {
                Coin2Value = coinsArray[1].Value;
                Coin2Amount = coinsArray[1].Amount;
            }
            else
            {
                Coin2Value = 0;
                Coin2Amount = 0;
            }
            if (length > 2)
            {
                Coin3Value = coinsArray[2].Value;
                Coin3Amount = coinsArray[2].Amount;
            }
            else
            {
                Coin3Value = 0;
                Coin3Amount = 0;
            }
            if (length > 3)
            {
                Coin4Value = coinsArray[3].Value;
                Coin4Amount = coinsArray[3].Amount;
            }
            else
            {
                Coin4Value = 0;
                Coin4Amount = 0;
            }
            if (length > 4)
            {
                Coin5Value = coinsArray[4].Value;
                Coin5Amount = coinsArray[4].Amount;
            }
            else
            {
                Coin5Value = 0;
                Coin5Amount = 0;
            }
        }

        public CoffeeMachineManager.Coins[] GetCoins()
        {
            return new CoffeeMachineManager.Coins[]
            {
                new CoffeeMachineManager.Coins {Value = Coin1Value, Amount = Coin1Amount},
                new CoffeeMachineManager.Coins {Value = Coin2Value, Amount = Coin2Amount},
                new CoffeeMachineManager.Coins {Value = Coin3Value, Amount = Coin3Amount},
                new CoffeeMachineManager.Coins {Value = Coin4Value, Amount = Coin4Amount},
                new CoffeeMachineManager.Coins {Value = Coin5Value, Amount = Coin5Amount},
            };
        }

    }
}
