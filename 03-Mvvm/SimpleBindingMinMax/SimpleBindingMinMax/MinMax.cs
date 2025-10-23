using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace SimpleBindingMinMax
{
    public class MinMax : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePorpertyChanged(string propertyname)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }


        int _zahl1;
        public int Zahl1
        {
            get { return _zahl1; }
            set
            {
                _zahl1 = value;
                RaisePorpertyChanged(nameof(Zahl1));
                RaisePorpertyChanged(nameof(MinValue));
            }
        }

        int _zahl2;
        public int Zahl2
        {
            get { return _zahl2; }
            set
            {
                _zahl2 = value;
                RaisePorpertyChanged(nameof(Zahl2));
                RaisePorpertyChanged(nameof(MinValue));
            }
        }

        public int MinValue
        {
            get { return Math.Min(Zahl1, Zahl2); }
        }

        public ICommand InitValues => new DelegateCommand(() => { Zahl1 = 1; Zahl2 = 1;  }, () => Zahl1 != 0 || Zahl2 != 0)
            .ObserveProperty();
    }
}
