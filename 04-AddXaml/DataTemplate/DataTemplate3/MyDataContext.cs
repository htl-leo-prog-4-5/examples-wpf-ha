using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataTemplate3
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
            new MyData()
            {
                StringData = "Hallo", IntData = 4711, BoolData = false,
                DetailData = new()
                {
                    new MyDataDetail() {StringDataDetail = "Detail11", IntDataDetail = 11, BoolDataDetail = false},
                    new MyDataDetail() {StringDataDetail = "Detail12", IntDataDetail = 12, BoolDataDetail = true}
                }
            },
            new MyData()
            {
                StringData = "Welt", IntData = 2022, BoolData = false,
                DetailData = new()
                {
                    new MyDataDetail() {StringDataDetail = "Detail21", IntDataDetail = 21, BoolDataDetail = false},
                    new MyDataDetail() {StringDataDetail = "Detail22", IntDataDetail = 22, BoolDataDetail = false}
                },
            },
            new MyData()
            {
                StringData = "Hello", IntData = 2023, BoolData = true,
                DetailData = new()
                {
                    new MyDataDetail() {StringDataDetail = "Detail31", IntDataDetail = 31, BoolDataDetail = true},
                    new MyDataDetail() {StringDataDetail = "Detail32", IntDataDetail = 32, BoolDataDetail = false}
                },
            },
            new MyData()
            {
                StringData = "World", IntData = 1900, BoolData = false,
                DetailData = new()
                {
                    new MyDataDetail() {StringDataDetail = "Detail41", IntDataDetail = 41, BoolDataDetail = true},
                    new MyDataDetail() {StringDataDetail = "Detail42", IntDataDetail = 42, BoolDataDetail = true}
                }
            }
        };

        #endregion
    }
}