using System.Windows.Input;
using ShiftGameCore;

namespace ShiftGame5x5
{
    public class ShiftGame : ShiftGameBase
    {
        public ShiftGame()
        {
            InitField(5, 5);
        }

        #region Properties

        public string Field00 => Field[0, 0];
        public string Field01 => Field[0, 1];
        public string Field02 => Field[0, 2];
        public string Field03 => Field[0, 3];
        public string Field04 => Field[0, 4];

        public string Field10 => Field[1, 0];
        public string Field11 => Field[1, 1];
        public string Field12 => Field[1, 2];
        public string Field13 => Field[1, 3];
        public string Field14 => Field[1, 4];

        public string Field20 => Field[2, 0];
        public string Field21 => Field[2, 1];
        public string Field22 => Field[2, 2];
        public string Field23 => Field[2, 3];
        public string Field24 => Field[2, 4];

        public string Field30 => Field[3, 0];
        public string Field31 => Field[3, 1];
        public string Field32 => Field[3, 2];
        public string Field33 => Field[3, 3];
        public string Field34 => Field[3, 4];

        public string Field40 => Field[4, 0];
        public string Field41 => Field[4, 1];
        public string Field42 => Field[4, 2];
        public string Field43 => Field[4, 3];
        public string Field44 => Field[4, 4];

        #endregion

        #region Operations

        #endregion

        #region Commands

        public ICommand SetField00Command => new DelegateCommand(() => { SetField(0, 0); }, () => CanSetField(0, 0));
        public ICommand SetField01Command => new DelegateCommand(() => { SetField(0, 1); }, () => CanSetField(0, 1));
        public ICommand SetField02Command => new DelegateCommand(() => { SetField(0, 2); }, () => CanSetField(0, 2));
        public ICommand SetField03Command => new DelegateCommand(() => { SetField(0, 3); }, () => CanSetField(0, 3));
        public ICommand SetField04Command => new DelegateCommand(() => { SetField(0, 4); }, () => CanSetField(0, 4));

        public ICommand SetField10Command => new DelegateCommand(() => { SetField(1, 0); }, () => CanSetField(1, 0));
        public ICommand SetField11Command => new DelegateCommand(() => { SetField(1, 1); }, () => CanSetField(1, 1));
        public ICommand SetField12Command => new DelegateCommand(() => { SetField(1, 2); }, () => CanSetField(1, 2));
        public ICommand SetField13Command => new DelegateCommand(() => { SetField(1, 3); }, () => CanSetField(1, 3));
        public ICommand SetField14Command => new DelegateCommand(() => { SetField(1, 4); }, () => CanSetField(1, 4));

        public ICommand SetField20Command => new DelegateCommand(() => { SetField(2, 0); }, () => CanSetField(2, 0));
        public ICommand SetField21Command => new DelegateCommand(() => { SetField(2, 1); }, () => CanSetField(2, 1));
        public ICommand SetField22Command => new DelegateCommand(() => { SetField(2, 2); }, () => CanSetField(2, 2));
        public ICommand SetField23Command => new DelegateCommand(() => { SetField(2, 3); }, () => CanSetField(2, 3));
        public ICommand SetField24Command => new DelegateCommand(() => { SetField(2, 4); }, () => CanSetField(2, 4));

        public ICommand SetField30Command => new DelegateCommand(() => { SetField(3, 0); }, () => CanSetField(3, 0));
        public ICommand SetField31Command => new DelegateCommand(() => { SetField(3, 1); }, () => CanSetField(3, 1));
        public ICommand SetField32Command => new DelegateCommand(() => { SetField(3, 2); }, () => CanSetField(3, 2));
        public ICommand SetField33Command => new DelegateCommand(() => { SetField(3, 3); }, () => CanSetField(3, 3));
        public ICommand SetField34Command => new DelegateCommand(() => { SetField(3, 4); }, () => CanSetField(3, 4));

        public ICommand SetField40Command => new DelegateCommand(() => { SetField(4, 0); }, () => CanSetField(4, 0));
        public ICommand SetField41Command => new DelegateCommand(() => { SetField(4, 1); }, () => CanSetField(4, 1));
        public ICommand SetField42Command => new DelegateCommand(() => { SetField(4, 2); }, () => CanSetField(4, 2));
        public ICommand SetField43Command => new DelegateCommand(() => { SetField(4, 3); }, () => CanSetField(4, 3));
        public ICommand SetField44Command => new DelegateCommand(() => { SetField(4, 4); }, () => CanSetField(4, 4));

        #endregion
    }
}