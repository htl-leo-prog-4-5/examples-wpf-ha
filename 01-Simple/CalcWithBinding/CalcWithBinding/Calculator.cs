using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CalcWithBinding
{
    public class Calculator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
        {
            var memberExpression = (MemberExpression) projection.Body;
            OnPropertyChanged(memberExpression.Member.Name);
        }

        const int Stacksize = 4;

        private int[] _stack = new int[Stacksize];

        public int Stack0
        {
            get => _stack[0];

            set
            {
                _stack[0] = value;
                OnPropertyChanged();
            }
        }

        public int Stack1
        {
            get => _stack[1];

            set
            {
                _stack[1] = value;
                OnPropertyChanged();
            }
        }

        public int Stack2
        {
            get => _stack[2];

            set
            {
                _stack[2] = value;
                OnPropertyChanged();
            }
        }

        public int Stack3
        {
            get => _stack[3];

            set
            {
                _stack[3] = value;
                OnPropertyChanged();
            }
        }


        public void Enter()
        {
            Stack3 = Stack2;
            Stack2 = Stack1;
            Stack1 = Stack0;
            Stack0 = 0;
        }

        public void Plus()
        {
            Pop(Stack1 + Stack0);
        }

        public void Minus()
        {
            Pop(Stack1 - Stack0);
        }

        public void Mul()
        {
            Pop(Stack1 * Stack0);
        }

        public void Div()
        {
            Pop(Stack1 / Stack0);
        }

        public void AddDigit(int digit)
        {
            Stack0 = Stack0 * 10 + digit;
        }

        private void Pop(int stack0)
        {
            Stack0 = stack0;
            Stack1 = Stack2;
            Stack2 = Stack3;
            Stack3 = 0;
        }
    }
}