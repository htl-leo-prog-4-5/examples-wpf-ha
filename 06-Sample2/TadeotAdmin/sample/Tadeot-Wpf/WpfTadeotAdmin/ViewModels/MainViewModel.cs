using Core.Contracts;
using Core.Entities.Visitors;
using Microsoft.Win32;
using Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMvvmBase;
using WpfTadeotAdmin.Views;

namespace WpfTadeotAdmin.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Visitor> Visitors { get; set; } = new ObservableCollection<Visitor>();


        private int _visitorsCount;
        public int VisitorsCount
        {
            get => _visitorsCount;
            set
            {
                _visitorsCount = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public RelayCommand DeleteVisitors { get; set; }
        public RelayCommand GenerateDemoVisitors { get; set; }
        public RelayCommand DownloadCSV { get; set; }

        public RelayCommand RegistrationConfig { get; set; }
        private IUnitOfWork _uow;


        public MainViewModel(IWindowController? controller) : base(controller)
        {
            _uow = new UnitOfWork();

            DeleteVisitors = new RelayCommand(
                async _ => await DeleteVisitorsAsync(),
                _ => VisitorsCount > 0);
            GenerateDemoVisitors = new RelayCommand(
                async _ => await GenerateDemoVisitorsAsync(),
                _ => VisitorsCount == 0);
            DownloadCSV = new RelayCommand(
                async _ => await DownloadCsvAsync(),
                _ => VisitorsCount > 0);
            RegistrationConfig = new RelayCommand(
                async _ => await OpenRegistrationConfigDialog(),
                _ => true);

        }

        private async Task OpenRegistrationConfigDialog()
        {
            var controller = new WindowController(new RegistrationConfigWindow());
            var viewModel = new RegistrationConfigViewModel(controller, _uow);
            await viewModel.LoadDataAsync();
            controller.ShowDialog(viewModel);
        }

        private async Task DownloadCsvAsync()
        {
            var visitors = await _uow.Visitors
                .GetAsync(null, 
                v => v.OrderBy(v => v.DateTime),
                nameof(Visitor.City));
            var lines = visitors
                .Select(visitor =>
                  visitor.Id + ";"
                + visitor.DateTime.ToShortDateString() + ";"
                + visitor.DateTime.ToShortTimeString() + ";"
                + visitor.Adults + ";"
                + visitor.InterestHIF + ";"
                + visitor.InterestHITM + ";"
                + visitor.InterestHBG + ";"
                + visitor.InterestHEL + ";"
                + visitor.InterestFEL + ";"
                + visitor.IsMale + ";"
                + visitor.City!.Name + ";"
                + visitor.City!.ZipCode + ";"
                + visitor.Comment + ";"
                + visitor.ReasonForVisit + ";"
                + visitor.SchoolType + ";"
                + visitor.SchoolLevel)
                .Aggregate((l1, l2) => l1 + "\n" + l2);

            lines = "id; date; time; adults; interestINF; interestHITM; interestHEL; interestHBG; interestFEL; isMale; city; zipCode; comment; reasonForVisit; schoolType; schoolLevel\n" + lines;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Csv file (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, lines);
            }
        }

        private async Task GenerateDemoVisitorsAsync()
        {
            await _uow.Visitors.GenerateTestDataAsync(300);
            await _uow.SaveChangesAsync();
            await LoadDataAsync();
        }

        private async Task DeleteVisitorsAsync()
        {
            var answer = MessageBox.Show("Wollen Sie wirklich alle Registrierungen löschen?",
                "Registrierungen löschen", 
                MessageBoxButton.YesNo,
                MessageBoxImage.Exclamation);
            if (answer == MessageBoxResult.Yes)
            {
                await _uow.Visitors.DeleteAllAsync();
                //await _uow.SaveChangesAsync();
                await LoadDataAsync();
            }
        }



        public async Task LoadDataAsync()
        {
            Visitors.Clear();
            var visitors = await _uow.Visitors.GetAllUntrackedAsync();
            foreach (var v in visitors)
            {
                Visitors.Add(v);
            }
            VisitorsCount = Visitors.Count;          
        }
    }
}
