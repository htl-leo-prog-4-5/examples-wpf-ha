using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CurrencyExchange
{
	public class Exchange : INotifyPropertyChanged
	{
		#region INPC 

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			/*
						var PropertyChanged = this.PropertyChanged;
						if (PropertyChanged != null)
						{
							PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
						}
			*/
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
		{
			var memberExpression = (MemberExpression)projection.Body;
			OnPropertyChanged(memberExpression.Member.Name);
		}

		#endregion

		#region Input

		double _value;

		public double Value
		{
			get => _value;

            set
			{
				_value = Math.Abs(value);
				OnPropertyChanged();
				OnPropertyChanged(() => ExchangeValue1);
				OnPropertyChanged(() => ExchangeValue2);
				OnPropertyChanged(() => Charge);
				OnPropertyChanged(() => TotalToPay);
			}
		}

		double _rate;

		public double Rate
		{
			get => _rate;

            set
			{
				_rate = Math.Abs(value);
				OnPropertyChanged();
				OnPropertyChanged(() => Charge);
				OnPropertyChanged(() => TotalToPay);
			}
		}

		private int _exchangeType;
		public int ExchangeType
		{
			get => _exchangeType;

            set
			{
				_exchangeType = value;
				OnPropertyChanged();
				OnPropertyChanged(() => CurrencyName1);
				OnPropertyChanged(() => CurrencyName2);
				OnPropertyChanged(() => ExchangeValue1);
				OnPropertyChanged(() => ExchangeValue2);
			}
		}

		#endregion

		enum EExchangeType
		{
			EuroToDollar=0,
			DollarToEuro=1,
			EuroToYen=2,
			YenToEuro=3
		}

		string[] _currencyName2 = { "Cent", "Cent", "Sen", "Cent" };
		string[] _currencyName1 = { "Dollar", "Euro", "Yen", "Euro" };
		double[] _currencyRate  = { 1.0626, 1/1.0626, 119.25, 1/119.25 };

		#region Output

		public string CurrencyName1 => _currencyName1[ExchangeType];

        public string CurrencyName2 => _currencyName2[ExchangeType];

        public double Charge => Math.Round(Value * Rate / 100.0,2);

        public double TotalToPay => Value + Charge;

        public double ExchangeRate => Math.Round(_currencyRate[ExchangeType], 4);

        public int ExchangeValue1 => (int) (Value* _currencyRate[ExchangeType]);

        public int ExchangeValue2 => (int) (((Value * _currencyRate[ExchangeType]) - ExchangeValue1) *100);

        #endregion
	}
}
