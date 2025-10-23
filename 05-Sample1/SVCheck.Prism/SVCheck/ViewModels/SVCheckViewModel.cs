using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace SVCheck.ViewModels
{
    public class SVCheckViewModel : BindableBase
    {
        private uint _sVNumber;
        private uint _sVYear;
        private uint _sVMonth;
        private uint _sVDay;
        private string _checkResult;

        #region crt

        #endregion

        #region Properties

        public uint SVNumber
        {
            get => _sVNumber;
            set => SetProperty(ref _sVNumber, value);
        }

        public uint SVYear
        {
            get => _sVYear;
            set => SetProperty(ref _sVYear, value);
        }

        public uint SVMonth
        {
            get => _sVMonth;
            set => SetProperty(ref _sVMonth, value);
        }

        public uint SVDay
        {
            get => _sVDay;
            set => SetProperty(ref _sVDay, value);
        }

        public string CheckResult
        {
            get => _checkResult;
            set => SetProperty(ref _checkResult, value);
        }

        #endregion

        #region Operations

        private bool TestSV(string svNumber)
        {
            int[] Gew = {3, 7, 9, 0, 5, 8, 4, 2, 1, 6};
            int i;
            int sum = 0;
            bool svOK = false;

            if (svNumber.Length == 10)
            {
                for (i = 0; i < 10 && svNumber[i] >= '0' && svNumber[i] <= '9'; i++)
                {
                    sum += Gew[i] * (svNumber[i] - '0');
                }

                svOK = (svNumber[3] - '0') == sum % 11;
            }

            return svOK;
        }

        private string ToSVNumber()
        {
            return $"{SVNumber:D04}{SVDay:D02}{SVMonth:D02}{SVYear%100:D02}";
        }

        public string InvalidYear => "ungültiges Jahr";
        public string InvalidMonth => "ungültiges Monat";
        public string InvalidDay => "ungültiger Tag";
        public string InvalidDate => "ungültiges Datum";
        public string InvalidSV => "falsch";
        public string SVOK => "OK";


        public void Check()
        {
            if (SVYear < 1900 || SVYear > 2050)
            {
                CheckResult = InvalidYear;
            }
            else if (SVMonth < 1 || SVMonth > 12)
            {
                CheckResult = InvalidMonth;
            }
            else if (SVDay < 1 || SVDay > 31)
            {
                CheckResult = InvalidDay;
            }
            else
            {
                try
                {
                    new DateTime((int) SVYear, (int)SVMonth, (int)SVDay);
                }
                catch (ArgumentOutOfRangeException)
                {
                    CheckResult = InvalidDate;
                    return;
                }

                if (TestSV(ToSVNumber()))
                {
                    CheckResult = SVOK;
                }
                else
                {
                    CheckResult = InvalidSV;
                }
            }
        }

        public bool CanCheck()
        {
            return true;
        }

        #endregion

        #region Commands

        public ICommand CheckCommand => new DelegateCommand(Check, CanCheck);

        #endregion
    }
}
