using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ShiftGameCore
{
    public abstract class ShiftGameBase : INotifyPropertyChanged
    {
        protected int SizeX { get; set; }
        protected int SizeY { get; set; }
        private int _moveCount = 0;
        protected string[,] Field;

        protected void InitField(int x, int y)
        {
            SizeX = x;
            SizeY = y;
            Field = new string[SizeX, SizeY];
            NewGame();
        }

        #region INPC

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        public int MoveCount
        {
            get => _moveCount;
            private set
            {
                _moveCount = value;
                RaisePropertyChanged();
            }
        }


        private bool IsSorted()
        {
            for (int x = 0; x < SizeX; x++)
            for (int y = 0; y < SizeY; y++)
                if (GetField(x, y) != (1 + x * SizeY + y))
                    return x == (SizeX - 1) && y == (SizeY - 1);
            return true;
        }

        private bool TrySetField(int index, int val)
        {
            int x = index / SizeY;
            int y = index % SizeY;

            if (!IsFieldEmpty(x, y)) return false;

            SetField(x, y, val.ToString());
            return true;
        }

        private void Swap(int x1, int y1, int x2, int y2)
        {
            string tmp = Field[x1, y1];
            SetField(x1, y1, Field[x2, y2]);
            SetField(x2, y2, tmp);
            MoveCount++;
        }

        private void SetField(int x, int y, string val)
        {
            Field[x, y] = val;
            RaisePropertyChanged($"Field{x}{y}");
        }

        protected void SetField(int x, int y)
        {
            if (x < (SizeX - 1) && IsFieldEmpty(x + 1, y))
            {
                Swap(x, y, x + 1, y);
            }
            else if (x > 0 && IsFieldEmpty(x - 1, y))
            {
                Swap(x, y, x - 1, y);
            }
            else if (y < (SizeY - 1) && IsFieldEmpty(x, y + 1))
            {
                Swap(x, y, x, y + 1);
            }
            else if (y > 0 && IsFieldEmpty(x, y - 1))
            {
                Swap(x, y, x, y - 1);
            }
        }

        private int GetField(int x, int y)
        {
            return IsFieldEmpty(x, y) ? -1 : int.Parse(Field[x, y]);
        }

        private bool IsFieldEmpty(int x, int y)
        {
            return string.IsNullOrEmpty(Field[x, y]);
        }

        protected bool CanSetField(int x, int y)
        {
            if (IsFieldEmpty(x, y)) return false;
            if (IsSorted()) return false;

            if (x < (SizeX - 1) && IsFieldEmpty(x + 1, y)) return true;
            if (x > 0 && IsFieldEmpty(x - 1, y)) return true;

            if (y < (SizeY - 1) && IsFieldEmpty(x, y + 1)) return true;
            if (y > 0 && IsFieldEmpty(x, y - 1)) return true;

            return false;
        }

        private void NewGame()
        {
            for (int x = 0; x < SizeX; x++)
            for (int y = 0; y < SizeY; y++)
                SetField(x, y, null);

            var rnd = new Random();

            for (int i = 1; i <= (SizeX * SizeY) - 1; i++)
            {
                while (!TrySetField(rnd.Next(0, SizeX * SizeY), i))
                {
                }
            }

            MoveCount = 0;
        }

        public ICommand NewGameCommand => new DelegateCommand(NewGame);
    }
}