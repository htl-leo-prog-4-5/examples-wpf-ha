using System.Collections.ObjectModel;
using Base.WpfMvvm;
using Core.Draw;
using Core.Entities;

namespace Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region crt

        public MainViewModel()
        {
        }

        #region Properties/Commands

        public IWindowNavigator? Controller { get; set; }

        public RelayCommand DeleteDrawingCommand => new RelayCommand(DeleteDrawing, () => SelectedDrawing != null);
        public RelayCommand LoadNewDrawingFileCommand => new RelayCommand(async () => await LoadNewDrawingFileAsync());

        private MyDrawing? _drawing = null;

        public MyDrawing? SelectedDrawing
        {
            get => _drawing;
            set => SetProperty(ref _drawing, value);
        }

        public ObservableCollection<MyDrawing> Drawings { get; } = new();

        #endregion


        #endregion

        #region Operations

        public async override Task InitializeDataAsync()
        {
            await Task.CompletedTask;
        }

        private async Task LoadNewDrawingFileAsync()
        {
            var filename = Controller?.ShowFileOpenDialog();

            if (string.IsNullOrEmpty(filename) == false)
            {
                AddDrawing(await DrawingParser.ParseDrawingAsync(filename));
            }
        }

        private void AddDrawing(MyDrawing drawing)
        {
            Drawings.Add(drawing);
            SelectedDrawing = drawing;
        }

        private void DeleteDrawing()
        {
            if (SelectedDrawing != null &&
                (Controller?.AskYesNoMessageBox("Warning", $"Delete '{SelectedDrawing.Name}?") ?? false))
            {
                var oldSelectedDrawing = SelectedDrawing;
                SelectedDrawing = null;
                Drawings.Remove(oldSelectedDrawing);
            }
        }

        #endregion
    }
}