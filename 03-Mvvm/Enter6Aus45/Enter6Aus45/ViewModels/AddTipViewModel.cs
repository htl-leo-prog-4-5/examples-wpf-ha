using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Enter6Aus45.Helpers;
using Enter6Aus45.Models;
using LottoQuickTip;

namespace Enter6Aus45.ViewModels
{
	public class AddTipViewModel : ViewModelBase
    {
        #region GUI Forward

        public Action CloseAction { get; set; }
        public Func<string, bool, string> BrowseFileNameFunc { get; set; }


        #endregion


        #region Properties

        private string _filename = @"c:\tmp\Enter6Aus45.csv";
        public string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                RaisePropertyChanged();
            }
        }
        private bool _overwrite = false;
        public bool Overwrite
        {
            get => _overwrite;
            set
            {
                _overwrite = value;
                RaisePropertyChanged();
            }
        }

        private UInt16 _range = 45;
        public UInt16 Range
        {
            get => _range;
            set
            {
                _range = value;
                RaisePropertyChanged();
            }
        }

        private LottoQuickTipModel _model = new LottoQuickTipModel();
        public LottoQuickTipModel Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Operations

        void GetTips()
        {
            var tips = new LottoTip().QuickTip(Range);
            Model.Tip1 = tips[0];
            Model.Tip2 = tips[1];
            Model.Tip3 = tips[2];
            Model.Tip4 = tips[3];
            Model.Tip5 = tips[4];
            Model.Tip6 = tips[5];
        }

        UInt16[] GetTipsArray()
        {
            return new UInt16[]
            {
                Model.Tip1,
                Model.Tip2,
                Model.Tip3,
                Model.Tip4,
                Model.Tip5,
                Model.Tip6
            };
        }

        void WriteToFile()
        {
            var lotto = new LottoTip();
            if (Overwrite)
                lotto.WriteToFile(Filename, GetTipsArray());
            else
                lotto.AppendToFile(Filename, GetTipsArray());
        }

        void BrowserFile()
        {
            string filename = BrowseFileNameFunc?.Invoke(Filename, true);
            if (filename != null)
                Filename = filename;
        }

        #endregion

        public ICommand GetTipsCommand => new DelegateCommand(GetTips);
        public ICommand WriteToFileCommand => new DelegateCommand(() =>
        {
            WriteToFile();
            CloseAction?.Invoke();
        }, () => Model.Tip1 != 0 && (Overwrite || System.IO.File.Exists(Environment.ExpandEnvironmentVariables(Filename))));
        public ICommand CloseCommand => new DelegateCommand(() => CloseAction?.Invoke());
        public ICommand BrowseFileCommand => new DelegateCommand(BrowserFile);


    }
}
