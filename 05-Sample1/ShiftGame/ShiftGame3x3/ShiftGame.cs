using ShiftGameCore;
using System.Windows.Input;

namespace ShiftGame3x3
{
    public class ShiftGame : ShiftGameBase
    {
        public ShiftGame()
        {
            InitField(3, 3);
        }

        #region Properties

        public string Field00 => Field[0, 0];
        public string Field01 => Field[0, 1];
        public string Field02 => Field[0, 2];

        public string Field10 => Field[1, 0];
        public string Field11 => Field[1, 1];
        public string Field12 => Field[1, 2];

        public string Field20 => Field[2, 0];
        public string Field21 => Field[2, 1];
        public string Field22 => Field[2, 2];

        #endregion

        #region Operations

        #endregion

        #region Commands

        public ICommand SetField00Command
        {
            get { return new DelegateCommand(() => { SetField(0, 0); }, () => CanSetField(0, 0)); }
        }

        public ICommand SetField01Command
        {
            get { return new DelegateCommand(() => { SetField(0, 1); }, () => CanSetField(0, 1)); }
        }

        public ICommand SetField02Command
        {
            get { return new DelegateCommand(() => { SetField(0, 2); }, () => CanSetField(0, 2)); }
        }

        public ICommand SetField10Command
        {
            get { return new DelegateCommand(() => { SetField(1, 0); }, () => CanSetField(1, 0)); }
        }

        public ICommand SetField11Command
        {
            get { return new DelegateCommand(() => { SetField(1, 1); }, () => CanSetField(1, 1)); }
        }

        public ICommand SetField12Command
        {
            get { return new DelegateCommand(() => { SetField(1, 2); }, () => CanSetField(1, 2)); }
        }

        public ICommand SetField20Command
        {
            get { return new DelegateCommand(() => { SetField(2, 0); }, () => CanSetField(2, 0)); }
        }

        public ICommand SetField21Command
        {
            get { return new DelegateCommand(() => { SetField(2, 1); }, () => CanSetField(2, 1)); }
        }

        public ICommand SetField22Command
        {
            get { return new DelegateCommand(() => { SetField(2, 2); }, () => CanSetField(2, 2)); }
        }

        #endregion
    }
}