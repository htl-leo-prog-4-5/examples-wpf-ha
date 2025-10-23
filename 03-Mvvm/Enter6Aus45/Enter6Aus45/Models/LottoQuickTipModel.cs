using System;
using Enter6Aus45.Helpers;

namespace Enter6Aus45.Models
{
    public class LottoQuickTipModel : BindableBase
    {
        private UInt16 _tip1;
        public UInt16 Tip1
        {
            get => _tip1;
            set
            {
                _tip1 = value;
                RaisePropertyChanged();
            }
        }
        private UInt16 _tip2;
        public UInt16 Tip2
        {
            get => _tip2;
            set
            {
                _tip2 = value;
                RaisePropertyChanged();
            }
        }
        private UInt16 _tip3;
        public UInt16 Tip3
        {
            get => _tip3;
            set
            {
                _tip3 = value;
                RaisePropertyChanged();
            }
        }
        private UInt16 _tip4;
        public UInt16 Tip4
        {
            get => _tip4;
            set
            {
                _tip4 = value;
                RaisePropertyChanged();
            }
        }
        private UInt16 _tip5;
        public UInt16 Tip5
        {
            get => _tip5;
            set
            {
                _tip5 = value;
                RaisePropertyChanged();
            }
        }
        private UInt16 _tip6;
        public UInt16 Tip6
        {
            get => _tip6;
            set
            {
                _tip6 = value;
                RaisePropertyChanged();
            }
        }
    }
}
