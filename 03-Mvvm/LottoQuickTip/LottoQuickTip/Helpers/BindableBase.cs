
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LottoQuickTip.Helpers
{
    public class BindableBase : INotifyPropertyChanged
    {
        #region INPC 

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
