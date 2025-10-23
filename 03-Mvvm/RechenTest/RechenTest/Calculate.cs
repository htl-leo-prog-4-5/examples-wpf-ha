using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RechenTest
{    
    
    class Calculate : INotifyPropertyChanged
    {
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var eventHandler = this.PropertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
        protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
        {
            var memberExpression = (MemberExpression)projection.Body;
            OnPropertyChanged(memberExpression.Member.Name);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public enum EOperation
        {
            PlusOp,
            MinusOp,
            MulOp,
            DivOp
        };

        EOperation _operation = EOperation.PlusOp;
  
        
        public bool IsNotPlus                  { get { return _operation != EOperation.PlusOp; } }
        public bool IsNotMinus                 { get { return _operation != EOperation.MinusOp; } }
        public bool IsNotMul                   { get { return _operation != EOperation.MulOp; } }
        public bool IsNotDiv                   { get { return _operation != EOperation.DivOp; } }


        public EOperation Operation 
        {
            get { return _operation; }
            set
            {
                _operation = value;
                OnPropertyChanged();
                OnPropertyChanged(() => IsNotPlus);
                OnPropertyChanged(() => IsNotMinus);
                OnPropertyChanged(() => IsNotMul);
                OnPropertyChanged(() => IsNotDiv);
                OnPropertyChanged(() => Result);
            }
        }

        int _val1;
        public int Val1 
        { 
            get { return _val1; }
            set { 
                _val1 = value; 
                OnPropertyChanged(); 
                OnPropertyChanged(() => Result); 
            } 
        }

        int _val2;
        public int Val2
        {
            get { return _val2; }
            set
            {
                _val2 = value;
                OnPropertyChanged();
                OnPropertyChanged(() => Result);
            }
        }
        public int Result
        {
            get 
            {
                switch (Operation)
                {
                    default:
                    case EOperation.PlusOp:     return _val1 + _val2;
                    case EOperation.MinusOp:    return _val1 - _val2;
                    case EOperation.MulOp:      return _val1 * _val2;
                    case EOperation.DivOp:      return _val1 / _val2;
                }
            }
        }
    }
}

