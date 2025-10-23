using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBindingINPC
{
	internal class PersonAddress : INotifyPropertyChanged
	{
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

		private string _grade;
		private string _firstName;
		private string _lastName;
		private string _street;
		private string _city;
		private string _zipCode;
		private string _country;

		private EAddressType _addressType;

		public enum EAddressType
		{
			UK=0,
			USA = 1,
			AUT = 2,
		}
		public string Address { get { return GetAddress(); } }

		public string FirstName
		{
			get => _firstName;
			set
			{
				_firstName = value;
				OnPropertyChanged();
				OnPropertyChanged(()=>Address);
			}
		}

		public string LastName
		{
			get
			{
				return _lastName;
			}

			set
			{
				_lastName = value;
				OnPropertyChanged();
				OnPropertyChanged(() => Address);
			}
		}

		public EAddressType AddressType
		{
			get
			{
				return _addressType;
			}

			set
			{
				_addressType = value;
				OnPropertyChanged();
				OnPropertyChanged(() => Address);
			}
		}

		public string Grade
		{
			get
			{
				return _grade;
			}

			set
			{
				_grade = value;
				OnPropertyChanged();
				OnPropertyChanged(() => Address);
			}
		}

		public string Street
		{
			get
			{
				return _street;
			}

			set
			{
				_street = value;
				OnPropertyChanged();
				OnPropertyChanged(() => Address);
			}
		}

		public string City
		{
			get
			{
				return _city;
			}

			set
			{
				_city = value;
				OnPropertyChanged();
				OnPropertyChanged(() => Address);
			}
		}

		public string ZipCode
		{
			get
			{
				return _zipCode;
			}

			set
			{
				_zipCode = value;
				OnPropertyChanged();
				OnPropertyChanged(() => Address);
			}
		}

		public string Country
		{
			get
			{
				return _country;
			}

			set
			{
				_country = value;
				OnPropertyChanged();
				OnPropertyChanged(() => Address);
			}
		}

		private string GetAddress()
		{
			switch(AddressType)
			{
				default:
				case EAddressType.USA: return GetAddressUSA();
				case EAddressType.UK: return GetAddressUK();
				case EAddressType.AUT: return GetAddressAUT();
			}
		}

		private string MyGrade()
		{
			if (string.IsNullOrEmpty(Grade))
				return "";

			return $"{Grade} ";
		}
		private string GetAddressUSA()
		{
			return $"{MyGrade()}{ FirstName } { LastName }\n{Street}\n{City}, {ZipCode}\n{Country}";
		}
		private string GetAddressUK()
		{
			return $"{MyGrade()}{ LastName  } { FirstName }\n{Street}\n{City}, {ZipCode}\n{Country}";
		}
		private string GetAddressAUT()
		{
			return $"{MyGrade()}{ LastName  } { FirstName }\n{Street}\n{ZipCode} {City}\n{Country}";
		}
	}
}
