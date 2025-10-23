using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NotenschnittMvvmWPF.Helpers;
using NotenschnittMvvmWPF.Models;

namespace NotenschnittMvvmWPF.ViewModels
{
    public class SchuelerViewModel : INotifyPropertyChanged
    {
        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        private Schueler Schueler { get; set; } = new Schueler();

        public string SchuelerName
        {
            get => Schueler.Name;
            set
            {
                Schueler.Name = value;
                RaisePropertyChanged();
            }
        }

        public int Mathematik
        {
            get => Schueler.Mathematik;
            set
            {
                Schueler.Mathematik = value;
                RaisePropertyChanged();
            }
        }

        public int Deutsch
        {
            get => Schueler.Deutsch;
            set
            {
                Schueler.Deutsch = value;
                RaisePropertyChanged();
            }
        }

        public int Englisch
        {
            get => Schueler.Englisch;
            set
            {
                Schueler.Englisch = value;
                RaisePropertyChanged();
            }
        }

        private string _schnitt;

        public string Schnitt
        {
            get => _schnitt;
            set
            {
                _schnitt = value;
                RaisePropertyChanged();
            }
        }

        static string BaseDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string _filenname = $"{BaseDirectory}\\Notenschnitt.csv";

        public string FileName
        {
            get => _filenname;
            set
            {
                _filenname = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Operations

        public void Load()
        {
            string alltext = File.ReadAllText(FileName);
            var split = alltext.Split(';');
            SchuelerName = split[0];
            Mathematik = int.Parse(split[1]);
            Deutsch = int.Parse(split[2]);
            Englisch = int.Parse(split[3]);
        }

        public bool CanLoad()
        {
            return string.IsNullOrEmpty(FileName) == false && File.Exists(FileName);
        }

        public void Save()
        {
            string alltext = $"{SchuelerName};{Mathematik};{Deutsch};{Englisch}";
            File.WriteAllText(FileName, alltext);
        }

        public bool CanSave()
        {
            return string.IsNullOrEmpty(FileName) == false;
        }

        public void Calculate()
        {
            Schnitt = System.Math.Round(((double)(Schueler.Englisch+Schueler.Deutsch+Schueler.Mathematik)) / 3.0,2).ToString();
        }

        #endregion

        bool IsInRange(int note)
        {
            return note >= 1 && note <= 5;
        }

        #region Commands

        public ICommand LoadCommand => new DelegateCommand(Load, CanLoad);
        public ICommand SaveCommand => new DelegateCommand(Save, CanSave);
        public ICommand CalcCommand => new DelegateCommand(Calculate, () => IsInRange(Englisch) && IsInRange(Mathematik) && IsInRange(Deutsch));

        #endregion
    }
}