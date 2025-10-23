using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace FindInFileAsync
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _filename = @"c:\tmp\t.txt";

        public string FileName
        {
            get => _filename;
            set
            {
                _filename = value;
                RaisePropertyChanged();
            }
        }

        private string _searchtext = "SREG";

        public string SearchText
        {
            get => _searchtext;
            set
            {
                _searchtext = value;
                RaisePropertyChanged();
            }
        }

        private string _resulText;

        public string ResultText
        {
            get => _resulText;
            set
            {
                _resulText = value;
                RaisePropertyChanged();
            }
        }

        private bool _searchIsRunning = false;
        private CancellationTokenSource _ctk;

        private async Task SearchAsync()
        {
            try
            {
                _searchIsRunning = true;
                _ctk = new CancellationTokenSource();
                await SearchAsync(FileName, SearchText, _ctk.Token);
            }
            catch (TaskCanceledException e)
            {
                ResultText += "\n=>Canceled";
            }
            finally
            {
                _searchIsRunning = false;
            }
        }

        private void StopSearch()
        {
            if (_ctk != null)
            {
                _ctk.Cancel();
            }
        }

        async Task<string> SearchAsync(string file, string search, CancellationToken ctk)
        {
            var contents = new StringBuilder();
            string nextLine;
            var lineCounter = 1;

            using (var reader = new StreamReader(file))
            {
                while ((nextLine = await reader.ReadLineAsync()) != null && !ctk.IsCancellationRequested)
                {
                    if (nextLine.Contains(search))
                    {
                        contents.Append($"{lineCounter}: ");
                        contents.Append(nextLine);
                        contents.AppendLine();
                    }

                    lineCounter++;

                    await Task.Delay(100, ctk);
                    ResultText = contents.ToString();
                }
            }

            return contents.ToString();
        }


        private bool CanSearch()
        {
            return !_searchIsRunning;
        }

        public ICommand StartSearchCommand => new DelegateCommand(async () => await SearchAsync(), CanSearch);
        public ICommand StopSearchCommand => new DelegateCommand(StopSearch, () => _searchIsRunning);
    }
}