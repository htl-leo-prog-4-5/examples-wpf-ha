using Core;
using Core.Entities;
using Microsoft.Win32;

namespace CNCViewerDesktop.ViewModels
{
    using Base.WpfMvvm;

    public class MainViewModel : BaseViewModel
    {
        private string? _gCodeFile;

        public RelayCommand ImportGcodeCommand { get; set; }

        private Pattern? _pattern;
        public Pattern? GCodePattern 
        { 
            get => _pattern; 
            set
            {
                _pattern = value;
                GCodeFile = $"{_pattern?.Name} {_pattern?.Width} x {_pattern?.Height}";
                OnPropertyChanged();
            }
        }

        public string? GCodeFile 
        { 
            get => _gCodeFile; 
            set
            {
                _gCodeFile = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            GCodePattern = Pattern.Demo;
            ImportGcodeCommand = new RelayCommand(
                async () => await ImportGCodeFileAsync(),
                () => true);
        }

        private async Task ImportGCodeFileAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GCode file (*.nc)|*.nc";
            if (openFileDialog.ShowDialog() == true)
            {
                var pattern = await GCodeParser.ParsePatternFromGcodeAsync(openFileDialog.FileName);
                GCodePattern = pattern;
            }
        }

        public async override Task InitializeDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}
