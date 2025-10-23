using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShowCSV
{
    class CSVDataContext : INotifyPropertyChanged
    {

        public CSVDataContext()
        {
            _content.Add(new CSVObject() { ProductCode = 4711, Descritpion = "Artikel 1", Retail = 47.11m });
            _content.Add(new CSVObject() { ProductCode = 4712, Descritpion = "Artikel 2", Retail = 47.12m });
            _content.Add(new CSVObject() { ProductCode = 4713, Descritpion = "Artikel 3", Retail = 47.13m });
            _content.Add(new CSVObject() { ProductCode = 4714, Descritpion = "Artikel 4", Retail = 47.14m });
            _content.Add(new CSVObject() { ProductCode = 4715, Descritpion = "Artikel 5", Retail = 47.15m });
            _content.Add(new CSVObject() { ProductCode = 4716, Descritpion = "Artikel 6", Retail = 47.16m });
            _content.Add(new CSVObject() { ProductCode = 4717, Descritpion = "Artikel 7", Retail = 47.17m });
            _content.Add(new CSVObject() { ProductCode = 4718, Descritpion = "Artikel 8", Retail = 47.18m });
            _content.Add(new CSVObject() { ProductCode = 4719, Descritpion = "Artikel 9", Retail = 47.19m });
        }

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

        List<CSVObject> _content = new List<CSVObject>();

        int _position = 0;

        public int Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
                OnPropertyChanged(() => ProductCode );
                OnPropertyChanged(() => Description );
                OnPropertyChanged(() => Retail);
            }
        }

        public void MoveFirst()
        {
            Position = 0;
        }
        public bool CanMoveFirst()
        {
            return Position != 0;
        }
        public void MovePrev()
        {
            Position = Position - 1;
        }
        public bool CanMovePrev()
        {
            return Position >  0;
        }
 
        public void MoveNext()
        {
            Position = Position+1;
        }
        public bool CanMoveNext()
        {
            return Position < _content.Count-1;
        }
        public void MoveLast()
        {
            Position = _content.Count - 1;
        }
        public bool CanMoveLast()
        {
            return Position != _content.Count - 1;
        }

        public ICommand MoveFirstCommand { get { return new DelegateCommand(MoveFirst, CanMoveFirst); } }
        public ICommand MovePrevCommand { get { return new DelegateCommand(MovePrev, CanMovePrev); } }
        public ICommand MoveNextCommand { get { return new DelegateCommand(MoveNext, CanMoveNext); } }
        public ICommand MoveLastCommand { get { return new DelegateCommand(MoveLast, CanMoveLast); } }


        public int ProductCode
        {
            get { return _content[Position].ProductCode; }
            set
            {
                _content[Position].ProductCode = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _content[Position].Descritpion; }
            set
            {
                _content[Position].Descritpion = value;
                OnPropertyChanged();
            }
        }
        public decimal Retail
        {
            get { return _content[Position].Retail; }
            set
            {
                _content[Position].Retail = value;
                OnPropertyChanged();
            }
        }
    }
}

