using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataTemplate1
{
    public class MyDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #region Properties

        public ObservableCollection<MyData> SomeData { get; } = new()
        {
            new MyData() {StringData = "Hallo", IntData = 4711, BoolData = false},
            new MyData() {StringData = "Welt", IntData = 2022, BoolData = false},
            new MyData() {StringData = "Hello", IntData = 2023, BoolData = true},
            new MyData() {StringData = "World", IntData = 1900, BoolData = false},
        };

        #endregion
    }
}