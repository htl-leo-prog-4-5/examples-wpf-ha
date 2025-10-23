namespace ValidationDemo1
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private uint   _deutsch       = 1;
        private uint   _mathematik    = 1;
        private uint   _englisch      = 1;
        private string _resultAvg     = string.Empty;
        private string _resultKlausel = string.Empty;

        public uint Deutsch
        {
            get => _deutsch;
            set
            {
                _deutsch = value;
                OnPropertyChanged();
            }
        }

        public uint Mathematik
        {
            get => _mathematik;
            set
            {
                _mathematik = value;
                OnPropertyChanged();
            }
        }

        public uint Englisch
        {
            get => _englisch;
            set
            {
                _englisch = value;
                OnPropertyChanged();
            }
        }

        public string ResultAvg
        {
            get => _resultAvg;
            set
            {
                _resultAvg = value;
                OnPropertyChanged();
            }
        }

        public string ResultKlausel
        {
            get => _resultKlausel;
            set
            {
                _resultKlausel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalculateCommand  => new DelegateCommand(DoCalculation, CanDoCalculation);

        static bool IsValidGrade(uint grade)
        {
            return grade >= 1 && grade <= 5;
        }

        private bool CanDoCalculation()
        {
            return IsValidGrade(Mathematik) && IsValidGrade(Englisch) && IsValidGrade(Deutsch);
        }

        private void DoCalculation()
        {
            if (IsValidGrade(Mathematik) && IsValidGrade(Englisch) && IsValidGrade(Deutsch))
            {
                bool   moreThan3 = Mathematik > 3 || Deutsch > 3 || Englisch > 3;
                bool   negativ   = Mathematik > 4 || Deutsch > 4 || Englisch > 4;
                double avg       = (Mathematik + Deutsch + Englisch) / 3.0;

                ResultAvg = $"Notenschnitt: {avg}";

                if (negativ)
                {
                    ResultKlausel = "nicht bestanden";
                }
                else
                {
                    if (avg <= 1.5 && !moreThan3)
                    {
                        ResultKlausel = "mit Auszeichnung bestanden";
                    }
                    else if (avg <= 2.0 && !moreThan3)
                    {
                        ResultKlausel = "mit gutem Erfolg bestanden";
                    }
                    else
                    {
                        ResultKlausel = "bestanden";
                    }
                }
            }
            else
            {
                ResultKlausel = "ungültige Eingabe";
                ResultAvg     = String.Empty;
            }
        }
    }
}