using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Reaktionsspiel
{
	public class Game : INotifyPropertyChanged
	{
		public Game()
		{
			NewGame();
		}

		#region INPC 

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
		{
			var memberExpression = (MemberExpression)projection.Body;
			OnPropertyChanged(memberExpression.Member.Name);
		}

		#endregion

		#region Properties

		int _moveCount = 0;
		public int MoveCount
		{
			get => _moveCount;
		    private set
			{
				_moveCount = value;
				OnPropertyChanged();
			}
		}

	    int _errors = 0;
	    public int Errors
	    {
	        get => _errors;
	        private set
	        {
	            _errors = value;
	            OnPropertyChanged();
	        }
	    }

	    private int _next1;
	    public int Next1
	    {
	        get => _next1;
	        private set
	        {
	            _next1 = value;
	            OnPropertyChanged();
	        }
	    }

	    private int _next2;
	    public int Next2
	    {
	        get => _next2;
	        private set
	        {
	            _next2 = value;
	            OnPropertyChanged();
	        }
	    }

	    private int _next3;
	    public int Next3
	    {
	        get => _next3;
	        private set
	        {
	            _next3 = value;
	            OnPropertyChanged();
	        }
	    }

	    private int _next4;
	    public int Next4
	    {
	        get => _next4;
	        private set
	        {
	            _next4 = value;
	            OnPropertyChanged();
	        }
	    }

        #endregion

        #region Operations

	    public void NewGame()
	    {
	        MoveCount = 0;
	        Errors = 0;
            SetNext();
        }

	    public bool CanNewGame()
	    {
	        return MoveCount != 0;
	    }

	    private int[] GetUniqueRandom(int min, int max)
	    {
	        var randomInts = new List<int>();
	        var rnd = new Random(DateTime.Now.Millisecond);
	        while (randomInts.Count <= (max-min))
	        {
	            while (true)
	            {
	                var nextRandom = rnd.Next(min, max+1);
	                if (!randomInts.Contains(nextRandom))
	                {
	                    randomInts.Add(nextRandom);
	                    break;
	                }
	            }
	        }

	        return randomInts.ToArray();
	    }

	    private void SetNext()
	    {
	        var rnds = GetUniqueRandom(1, 4);

            Next1 = MoveCount + rnds[0];
	        Next2 = MoveCount + rnds[1];
	        Next3 = MoveCount + rnds[2];
	        Next4 = MoveCount + rnds[3];
        }

        public void Press(int index)
	    {
	        switch (index)
	        {
                case 0: PressOK(Next1 == MoveCount + 1); break;
	            case 1: PressOK(Next2 == MoveCount + 1); break;
	            case 2: PressOK(Next3 == MoveCount + 1); break;
	            case 3: PressOK(Next4 == MoveCount + 1); break;
	        }
        }

	    private void PressOK(bool ok)
	    {
	        if (ok)
	        {
	            MoveCount++;
            }
            else
	        {
	            Errors++;
	        }
            SetNext();
	    }

        #endregion

        #region Commands

        public ICommand NewGameCommand		{ get { return new DelegateCommand(NewGame,CanNewGame); } }

		#endregion
	}
}
