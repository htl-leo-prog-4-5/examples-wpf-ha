using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Views;

namespace CoffeeMachine
{
    public class CoffeeMachineManager
    {
        public class Coins
        {
            public decimal Value { get; set; }
            public uint Amount { get; set; }
        }

        private class Status
        {
            public string FileName { get; set; } = @"%tmp%\CoffeMachine.csv";

            public Coins[] Coins { get; set; } = new Coins[]
            {
                new Coins {Value = 0.1m, Amount = 0},
                new Coins {Value = 0.2m, Amount = 0},
                new Coins {Value = 0.5m, Amount = 0},
                new Coins {Value = 1m, Amount = 0},
                new Coins {Value = 2m, Amount = 0}
            };

            public bool Initialized { get; set; } = false;
        }

        static Status _status = new Status();

        public string CurrentFileName => _status.FileName;
        public Coins[] CurrentCoins => _status.Coins.ToArray();

        public IEnumerable<Coins> ReadFromFile(string filename)
        {
            var filecontent = System.IO.File.ReadAllLines(Environment.ExpandEnvironmentVariables(filename));
            var coins = new List<Coins>();

            foreach (var line in filecontent)
            {
                var col = line.Split(';');
                coins.Add(new Coins() {Value = decimal.Parse(col[0]), Amount = uint.Parse(col[1])});
            }

            _status.FileName = filename;
            _status.Coins = coins.OrderBy((e) => e.Value).ToArray();
            _status.Initialized = true;

            return _status.Coins.ToArray();
        }

        public void WriteToFile(string filename, IEnumerable<Coins> coins)
        {
            var sb = new StringBuilder();
            foreach (var coin in coins)
            {
                sb.Append(coin.Value.ToString());
                sb.Append(";");
                sb.Append(coin.Amount.ToString());
                sb.AppendLine();
            }
            System.IO.File.WriteAllText(Environment.ExpandEnvironmentVariables(filename), sb.ToString());
            _status.FileName = filename;
            _status.Coins = coins.OrderBy( (e) => e.Value).ToArray();
            _status.Initialized = true;
        }

        private void AddCoins(IEnumerable<Coins> from, IList<Coins> to)
        {
            foreach (var coin in from)
            {
                var inlist = to.FirstOrDefault(c => c.Value == coin.Value);
                if (inlist == null)
                {
                    to.Add(new Coins() {Amount = coin.Amount, Value = coin.Value});
                }
                else
                {
                    inlist.Amount += coin.Amount;
                }
            }
        }

        public IEnumerable<Coins> Sell(decimal price, IEnumerable<Coins> givencoins)
        {
            if (!_status.Initialized)
                ReadFromFile(CurrentFileName);

            var returncoins = new List<Coins>();

            givencoins = givencoins.OrderBy((e) => e.Value).ToArray();

            decimal totalgiven = 0;
            foreach (var c in givencoins)
                totalgiven +=  c.Value * c.Amount;

            if (totalgiven < price)
                throw new Exception("to less money given");

            var restmoney = totalgiven - price;

            var newdepot = new List<Coins>();
            AddCoins(_status.Coins, newdepot);
            AddCoins(givencoins, newdepot);

            foreach (var c in newdepot.OrderByDescending(c=>c.Value))
            {
                var count = (uint) (restmoney / c.Value);
                if (count > c.Amount)
                    count = c.Amount;

                if (count > 0)
                {
                    c.Amount -= count;
                    restmoney -= c.Value * count;
                    returncoins.Add(new Coins() {Amount = count, Value = c.Value});
                }
            }

            if (restmoney > 0)
                throw new Exception("no exchangemoney");

            WriteToFile(CurrentFileName, newdepot);

            return returncoins.OrderBy(c => c.Value);
        }
    }
}